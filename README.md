# 🎟️ Event Flow — Full-Stack Cloud-Ready Ticketing Platform

A modern event listing and ticket sales platform built with .NET Core and Angular, fully containerized for scalable deployments.

- **.NET Core** (API/backend)
- **Angular** (frontend)
- **Docker** (containerization and CI/CD)

Supports event browsing, ticket sales, background processing, analytics, and scalable deployment to Azure (AKS, ACR, SQL, Redis).

---

## 🚀 Features

- **.NET Core API**
  - Clean architecture using Repository-Service pattern
  - NHibernate ORM with SQLite (default) or Azure SQL/PostgreSQL
  - RESTful endpoints for events and tickets

- **Angular Frontend**
  - Modern SPA for browsing events, managing tickets
  - Responsive design and UI routing
  - Dockerized for deployment

- **Unit Testing**
  - Backend tested with `xUnit` and `Moq`
  - Test runner runs inside Docker and outputs `.trx` results

- **Containerized Everything**
  - API, frontend, and test suite run in isolated containers
  - Supports local dev and production deployments

- **Cloud-Ready Deployment**
  - Designed for Azure: AKS, Azure SQL/Postgres, Redis, ACR
  - Scalable architecture with CI/CD in mind

- **Architecture Diagrams**
  - Mermaid diagrams in `mermaid.md` for deployment flow and app logic

---

## 🗂️ Project Structure

```
eventinformation/
├── EventApiSolution/
│   ├── EventApi/              # .NET Core API
│   ├── EventApi.Tests/        # xUnit Tests
│   ├── Dockerfile             # API Dockerfile
│   ├── Dockerfile.tests       # Test runner
├── eventsui/                  # Angular frontend
│   ├── src/
│   ├── Dockerfile
├── docker-compose.yml         # Docker Orchestration
├── test-results/              # Test output
├── mermaid.md                 # Mermaid architecture diagrams
└── README.md
```

---

## ⚙️ Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---

## ▶️ Run the App Locally

```bash
docker compose up
```

- API: http://localhost:8080  
- Frontend: http://localhost:4200

---

## 🧪 Run Backend Tests

### Using Docker (recommended)

```bash
docker compose up eventapi-tests
```

- Outputs test results to `./test-results/test_results.trx`

### Manually via CLI

```bash
cd EventApiSolution
dotnet test --no-restore --logger "trx;LogFileName=results/test_results.trx"
```

---

## 🐳 Docker Compose Services Overview

```yaml
services:
  eventapi:
    platform: linux/amd64
    build:
      context: ./EventApiSolution
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    volumes:
      - ./EventApiSolution/Database/skillsAssessmentEvents.db:/app/Database/skillsAssessmentEvents.db

  eventapi-tests:
    build:
      context: ./EventApiSolution
      dockerfile: Dockerfile.tests
    volumes:
      - ./test-results:/src/EventApi.Tests/results
    command: >
      sh -c "dotnet test --no-restore --logger 'trx;LogFileName=results/test_results.trx' &&
             cat results/test_results.trx &&
             tail -f /dev/null"

  eventsui:
    build:
      context: ./eventsui
      dockerfile: Dockerfile
    ports:
      - "4200:80"
```

---
## 🌐 Frontend Feature

### 📄 Event Listing Page

- [ ] **Fetch Events**  
  Call `/api/events` to retrieve upcoming events.

- [ ] **Display Event Table**  
  Render a responsive and accessible table with the following columns:
  - Event Name
  - Start Date & Time
  - End Date & Time

- [ ] **Sorting Functionality**  
  Allow users to sort the event list by:
  - Event Name (alphabetical)
  - Start Date & Time (chronological)

- [ ] **Error Handling**  
  Show a user-friendly error message if the API call fails.

### 📊 Sales Summary Page

- [ ] **Top 5 Events by Ticket Count**  
  Fetch and display the top 5 events with the most tickets sold.

- [ ] **Top 5 Events by Revenue**  
  Fetch and display the top 5 events by total dollar amount in sales.


### 🧭 Navigation & Structure

- [ ] **Routing**  
  Implement routing between Event Listing and Sales Summary pages.

- [ ] **Navbar/Header**  
  Add a simple navigation bar or menu to switch between pages.
---
## 🖥️ Backend Feature Requirements

### 📦 API Development (ASP.NET Core, .NET 8)

- [ ] **/api/events Endpoint**  
  Implement an endpoint that returns a list of upcoming events within the next:
  - 30 days
  - 60 days
  - 180 days

- [ ] **Event Model**  
  Ensure each event includes:
  - Id
  - Name
  - Start Date & Time
  - End Date & Time


### 🔁 Services & Dependency Injection

- [ ] **IEventService**  
  Interface for fetching event data via dependency injection.

- [ ] **ITicketService**  
  Interface for ticket-related data:
  - List tickets by Event ID
  - Top 5 events by ticket count
  - Top 5 events by total sales amount


### 🗃️ Data Access Layer (NHibernate)

- [ ] **Event Repository**  
  Access and map the Events table using NHibernate.

- [ ] **Ticket Repository**  
  Access and map Tickets, with optimized queries for top-selling events.

- [ ] **NHibernate Mapping Files**  
  Provide entity-to-table mappings for all relevant database models.

### 🧪 Unit Testing (MSTest / NUnit)

- [ ] **Event Service Tests**  
  Unit tests for `IEventService`.

- [ ] **Ticket Service Tests**  
  Unit tests for `ITicketService`.
---

## 📊 Architecture

See [`mermaid.md`](./mermaid.md) for infrastructure and flow diagrams.

---

## 🛠️ Customization

### Favicon

Replaced `eventsui/src/favicon.ico` with `.svg` file.

### Database

- Default: SQLite (local dev)
- To use Azure SQL or PostgreSQL, update `appsettings.json` and connection strings.

---

## 📦 Deployment Notes

- **Azure Ready:** Easily deployable to AKS, ACR, Azure SQL, and Redis
- **CI/CD Pipelines:** Recommend GitHub Actions or Azure Pipelines for building and deploying Docker images

---

## 📝 Assumptions, Challenges & Reflections

One of the key assumptions I made early in this project was that SQLite would integrate seamlessly with Apple Silicon (M1/M2) chips during local development. However, this quickly turned out to be problematic. Running the app natively on macOS with Apple Silicon led to SQLite compatibility issues and runtime exceptions that repeatedly blocked my progress.

I spent a significant amount of time researching various workarounds, including testing multiple versions of NHibernate, experimenting with different .NET runtime builds, and trying out various SQLite providers. This involved countless installations, downgrades, upgrades, and even attempts to recompile native binaries from source. Despite all these efforts, the problems persisted, and I was stuck until I could find alternative solutions or a stable compatibility layer.

To overcome this, I shifted my focus to containerizing the entire application using Docker. This approach resolved the compatibility issues by providing a consistent, reproducible environment that worked regardless of the host machine’s architecture. Containerizing took some time as well, since it required adjustments to the development workflow, fine-tuning the Dockerfile, and making sure the application, database, and NHibernate configurations all functioned correctly inside the container. Ultimately, this solution unblocked local development and also aligned well with my long-term vision of enabling scalable, cloud-ready deployments.

From a design perspective, I intentionally structured the backend and frontend into clearly separated domains. I believe this architectural choice supports better scalability, improved testability, and easier future migration to microservices if the app grows. Initially, I assumed that .NET’s ecosystem and tooling would make the backend implementation straightforward. While that was true to some extent, the platform-specific SQLite issues introduced unexpected friction.

Despite these setbacks, the challenge deepened my understanding of:

- How platform-dependent native libraries behave differently across operating systems  
- The critical importance of multi-platform Docker support  
- Why adopting a container-first development approach is essential for cross-platform reliability  

Containerization emerged not just as a workaround, but as the best foundational approach for future scalability, CI/CD integration, and cloud hosting.

---

### Technical Insights on the Issues Encountered

- .NET, NHibernate, and SQLite all rely on native libraries that may behave differently or require special setup on macOS compared to Linux or Windows.
- Apple Silicon (ARM) Macs introduce unique challenges because many native dependencies are built primarily for Intel/x64 architectures.
- Common errors I faced included missing or incompatible SQLite native libraries (such as `e_sqlite3`), architecture mismatch errors (ARM vs x64), and build or runtime failures related to these differences.
- These environment-specific issues meant my code could work perfectly on one machine but fail on another, making troubleshooting especially difficult since the problems were outside the code itself.

This experience reinforced how important it is to validate assumptions early and embrace containerization not only as a compatibility fix but as a strategic foundation for modern software development.

---
## 🎨 TailwindCSS + Docker Friction

In addition to backend-specific problems, I ran into unexpected issues with TailwindCSS when working inside the Docker container.

Tailwind would behave inconsistently depending on the version — some versions failed to compile properly, while others introduced subtle formatting bugs that didn’t exist outside the container. These problems weren’t obvious at first and took time to track down. I went through multiple cycles of upgrading, downgrading, clearing caches, and rebuilding containers just to stabilize the styling behavior.

This experience highlighted a subtle but important challenge in frontend development: even tools that “just work” locally can behave differently inside isolated environments like Docker. It was a good reminder that aligning tooling versions across environments is just as important as aligning backend dependencies.

Ultimately, finding a stable TailwindCSS version that worked reliably inside Docker was a big relief and allowed frontend development to move forward without further interruptions.

---

## 👤 Author

**Ryan Kiel Baluyut**  
Software Developer | Full-Stack Engineer | DevOps Engineer
Calgary, Alberta, Canada