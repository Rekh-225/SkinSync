# SkinSync

SkinSync is an **offline, rule-based skincare recommendation system** built with **C#/.NET**.  
It generates a simple skincare routine (cleanser, moisturizer, sunscreen) based on **skin type**, **weather condition**, and optional **skin concerns**, using **deterministic and explainable rules**.

This repository currently contains **Version 1 (CLI + JSON)** and is designed to evolve into a **full web system (ASP.NET Core Web API + React)**.

---

## Disclaimer

SkinSync provides **educational and informational** recommendations only.  
It is **not medical advice** and does not diagnose or treat skin conditions.

---

## Features

### Current (Version 1 — Offline)
- ✅ **Offline-first**: no external APIs required
- ✅ **Deterministic recommendations**: same input → same output
- ✅ **Explainable selection**:
  - Exact match (SkinType + Weather)
  - SkinType fallback
  - Weather fallback
  - Default fallback (Normal + Moderate)
- ✅ **JSON-driven dataset**: routines stored in `Resources/routines.json`
- ✅ **Concern tips** (rule-based):
  - `Acne`
  - `Sensitivity`
- ✅ Clean separation of responsibilities (Core / Data / CLI)

### Planned (Next Versions)
- 🔜 Unit tests (xUnit)
- 🔜 ASP.NET Core Web API (`POST /recommendation`)
- 🔜 React frontend (website UI)
- 🔜 Real-time weather via external API
- 🔜 User profiles + saved routines/history
- 🔜 AI-powered explanations (optional)

---

## Tech Stack

- **Language:** C#
- **Framework:** .NET (developed on .NET 9)
- **App:** Console / CLI (current)
- **Storage:** JSON (`System.Text.Json`)
- **Architecture style:** modular, OOP + separation of concerns
- **Future:** ASP.NET Core Web API + React

---

## Project Structure

```txt
SkinSync.Cli/
├─ Core/
│  ├─ Enums/         # SkinType, WeatherType, SkinConcern
│  ├─ Models/        # SkinRoutine, RecommendationRequest/Result
│  ├─ Engine/        # RecommendationEngine, RoutinePrinter (CLI helper)
├─ Data/             # RoutineRepository (JSON loading + fallback selection)
├─ Resources/        # routines.json
└─ Program.cs        # demo runner (will later become interactive / API-backed)
