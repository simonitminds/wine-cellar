# 🍷 Kaer Morhen's Finest: A Witcher's Wine Cellar 🧪

## "Wind's howling... Time for a drink!" - Geralt of Rivia

Welcome to the most ambitious crossover between monster slaying and wine tasting! This project is a digital wine cellar management system, lovingly crafted for storing Witcher brews and keeping track of Yennefer's ever-growing collection of rare vintages.

## 🗡️ Features

- **Potion Tracking**: Keep your Thunderbolt and Swallow potions organized. No more mixing up your hangover cure with your combat enhancers!
- **Vintage Management**: Because Yennefer insists on knowing the exact year of her Toussaint red.
- **Gwent Card Pairing**: Recommend the perfect brew for your next Gwent tournament.
- **Roach-Proof Storage**: Our advanced algorithms ensure your storage solutions are immune to Roach's teleportation shenanigans.

## 🐺 Getting Started

1. Clone this repository faster than a Bruxa on a moonless night.
2. Run `dotnet restore` to gather ingredients (dependencies).
3. Brew your database with `dotnet ef database update`. Don't worry, it's less volatile than Shani's experiments.
4. Launch the application with `dotnet run`. It's easier than convincing Dandelion to stop singing.

## 🧙‍♀️ API Endpoints

- `POST /api/brews`: Add a new brew. Alchemy skills required.
- `GET /api/cellar`: View your collection. Try not to get too thirsty.
- `DELETE /api/empties`: Clean up after a wild night at the Passiflora.

## 🎮 Technologies

- **ASP.NET Core**: Because even Witchers need a robust backend.
- **Entity Framework Core**: Mapping relations more intricately than the politics of the Northern Kingdoms.
- **JWT Authentication**: Secured tighter than Phillipa Eilhart's schemes.
- **Swagger/OpenAPI**: Documentation clearer than Triss Merigold's intentions.
- **Carter Modules**: Organizing code better than Vesemir organizes Witcher trainees.

## 🏰 Architecture

Our architecture is as carefully crafted as Vesemir's training regimen, combining the flexibility of Carter modules with the purity of Clean Architecture:

### 🗡️ Clean Architecture: The Witcher's Code

Our project structure follows Clean Architecture principles, as unbreakable as a Witcher's code:

1. **Domain Layer**: The core of our application, like a Witcher's mutations.
   - Contains all entity models (e.g., `Brew`, `Ingredient`, `CellarShelf`)
   - Defines interfaces for repositories

2. **Infrastructure Layer**: The tools of our trade, like a Witcher's equipment.
   - **Persistence**: 
     - Includes `DbContext` and entity configurations
     - Implements repository interfaces defined in the domain layer
   - **External Services**: Integrations with third-party services (e.g., "Continent-wide Brew Catalog API")

3. **Presentation Layer**: The face of our application, like Dandelion's ballads about Witchers.
   - Carter modules live here, defining API endpoints
   - Handles request/response models and validations

## 🧪 Testing

Run tests with `dotnet test`. Remember, a Witcher always verifies their potions before drinking!

## 🎵 Contributing

Contributions are welcome! But remember:

1. Evil is evil. Lesser, greater, middling… Makes no difference. If I'm to choose between contributing and not contributing, I'd rather contribute.
2. Mistakes are like Drowners - best dealt with quickly and decisively.
3. Your pull requests will be tossed about by the winds of continuous integration like a boat on a storm. Steady as she goes!

## 📜 License

This project is licensed under the "Witcher's Code" - Use it well, use it wisely, and may it serve you as faithfully as a silver sword against a Striga.

---

"I'm extremely ridiculous but passionate about my work. Much like Dandelion, but with better version control." - Project Maintainer

Remember: Code responsibly. Don't drink and hex.
