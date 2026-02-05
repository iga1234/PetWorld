# PetWorld - AI Pet Store Assistant

Aplikacja sklepu zoologicznego z inteligentnym chatbotem AI, który pomaga klientom w wyborze produktów dla zwierząt.

## Opis

PetWorld to aplikacja webowa umożliwiająca interakcję z asystentem AI, który:
- Rekomenduje produkty na podstawie potrzeb klienta
- Filtruje produkty po kategorii, typie zwierzęcia lub rodzaju produktu
- Prowadzi naturalną konwersację po polsku
- Pamięta kontekst rozmowy w ramach sesji

Aplikacja wykorzystuje architekturę **Writer-Critic**, gdzie:
- **Writer Agent** generuje odpowiedzi na podstawie zapytania klienta
- **Critic Agent** weryfikuje poprawność odpowiedzi (czy produkty istnieją, czy kategorie są prawidłowe)
- System iteruje do 3 razy, aż odpowiedź zostanie zaakceptowana

## Technologie

- **.NET 9** - framework backendowy
- **Blazor Server** - interaktywny UI
- **Microsoft Agent Framework** - framework do tworzenia agentów AI
- **Entity Framework Core** - ORM
- **MySQL 8.0** - baza danych
- **Docker** - konteneryzacja

## Architektura

Projekt wykorzystuje **Clean Architecture** z podziałem na warstwy:

```
PetWorld/
├── PetWorld.Domain/           # Warstwa domenowa
│   ├── Entities/              # Encje (Product, ChatSession)
│   └── IRepository/           # Interfejsy repozytoriów
│
├── PetWorld.Application/      # Warstwa aplikacji
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Interfaces/            # Interfejsy serwisów
│   └── Services/              # Logika biznesowa (ChatService)
│
├── PetWorld.Infrastructure/   # Warstwa infrastruktury
│   ├── AI/                    # Agenci AI (Writer, Critic, Orchestrator)
│   ├── Data/                  # DbContext, repozytoria, seeder
│   └── DependencyInjection.cs # Konfiguracja DI
│
└── PetWorld.Web/              # Warstwa prezentacji
    ├── Components/            # Komponenty Blazor (Chat, History)
    └── Program.cs             # Entry point aplikacji
```

## Wymagania

- Docker Desktop
- Klucz API OpenAI

## Uruchomienie

### 1. Zainstaluj i uruchom Docker

- **Windows/macOS**: Zainstaluj i uruchom [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- **Linux**: Zainstaluj [Docker Engine](https://docs.docker.com/engine/install/)

### 2. Uzupełnij API Key

W pliku `PetWorld.Web/appsettings.json` wpisz swój klucz OpenAI:

```json
"OpenAI": {
  "ApiKey": "TWÓJ_KLUCZ_API",
  "ModelId": "gpt-4"
}
```

### 3. Uruchom aplikację

```bash
docker compose up -d
```

### 4. Otwórz w przeglądarce

Aplikacja będzie dostępna pod adresem: http://localhost:5000
