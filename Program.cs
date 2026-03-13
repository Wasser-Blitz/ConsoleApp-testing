using RestSharp;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
    string currentMode = "test";
    string Userinput = string.Empty;
Console.BackgroundColor = ConsoleColor.Black;
Console.Clear();
while (true)
{
    currentMode = "test";
    Userinput = string.Empty;
    string UserMode = string.Empty;
    if (currentMode == "test")
    {

        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("To open the Settings Type: Settings \nTo open the Recipe app Type: Recipe");
        Console.WriteLine("--------------------------------------------");
        UserMode = Console.ReadLine();
        if (UserMode == "Recipe")
        {
            currentMode = null;
        }
        else if (UserMode == "Settings")
        {
            currentMode = "Settings";
        }
        if (currentMode == null)
        {
            Console.WriteLine("\n--------------------------------------------------------");
            Console.WriteLine("To search by name type: Name" + "\n" + "To search by ingredient type: Ingredient" + "\n" + "To search by category type: Category" + "\n" + "To search by Country type: Area" + "\nFor a Random Dish type: Surprise Me");
            Console.WriteLine("-------------------------------------------------------");
            Userinput = Console.ReadLine(); //reads waht the user typed
            Console.Clear();// text gets deleted if you write a new message
        }
        else if (currentMode == "Settings") 
        {
            Console.WriteLine("Type The color you want for the Backround or type Back to get Back into the Menu");
            string BackroundColor = Console.ReadLine();
            if (BackroundColor == "Red")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Blue")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                currentMode = "test";
                continue; 
            }

            else if (BackroundColor == "Green")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "DarkBlue")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "LightBlue")
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Yellow")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Magenta")
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "White")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Black")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Gray")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Dark Gray")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Clear();
                currentMode = "test";
                continue;
            }
            else if (BackroundColor == "Back") 
            {
                continue;
            }
        }
    }

    //NAME START
    if (Userinput == "Name")
    {
        Console.Clear();
        Userinput = null;
        Console.WriteLine("\n-------------------------------------------------------------------------------------------------");
        Console.WriteLine("Write the name of a dish you want to make or type Back if you want to search with another filter.");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        string Userline = Console.ReadLine();
        if (Userline != "Back")
        {
            async Task<Meals> GetMealsByName()
            {
                var client = new RestClient("https://www.themealdb.com/api/json/v1/1/search.php?s=" + Userline);
                var response = await client.GetAsync<Meals>(new RestRequest());

                return response;

            }

            var data = await GetMealsByName();

            Console.WriteLine("Count: " + (data.LsMeals != null ? data.LsMeals?.Count : "0"));

            foreach (var meal in data.LsMeals ?? [])
            {

                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("\n" + "Name: " + meal.Gericht + "\n");

                var propVegan = meal.Vegane_Variante;
                if (!string.IsNullOrWhiteSpace(propVegan))
                    Console.WriteLine("Vegan Alternative? " + meal.Vegane_Variante);

                var propTag = meal.Tag;
                if (propTag != null)
                    Console.WriteLine("Tags: " + meal.Tag);

                var propHerkunft = meal.Herkunft;
                if (!string.IsNullOrWhiteSpace(propHerkunft))
                    Console.WriteLine("Origin: " + meal.Herkunft);

                var propBeschreibung = meal.Beschreibung;
                if (!string.IsNullOrWhiteSpace(propBeschreibung))
                    Console.WriteLine("Discription: " + meal.Beschreibung);

                Console.WriteLine("\n" + "-- Ingredients:");

                foreach (var property in meal.GetType().GetProperties())
                {
                    var propName = property.Name;
                    var propValue = meal.GetType().GetProperty(propName)?.GetValue(meal)?.ToString();

                    if (propName.Contains("Zutat") && !string.IsNullOrWhiteSpace(propValue))
                    {
                        if (int.TryParse(propName.Replace("Zutat", string.Empty), out int index))
                        {
                            var quantity = meal.GetType().GetProperty("Anzahl" + index)?.GetValue(meal)?.ToString();
                            Console.Write(quantity + " " + propValue + "\n");
                        }
                    }
                }
                Console.WriteLine("\n-----STEP BY STEP INSTRUCTIONS-----");
                var propAnleitung = meal.Anleitung;
                if (!string.IsNullOrWhiteSpace(propAnleitung))
                    Console.WriteLine(meal.Anleitung + "\n");
            }
        }
        else if (Userline == "Back")
        {
            currentMode = null;
            continue;
        }
    }

    //INGREDIENTS START
    else if (Userinput == "Ingredient")
    {

        Console.Clear();
        Userinput = null;
        Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Write the name of an Ingredient you want to cook with or type Back if you want to search with another filter.");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
        string Userline = Console.ReadLine();
        if (Userline != "Back")
        {
            async Task<Meals> GetMealsByIngredient()
            {
                var client1 = new RestClient("https://www.themealdb.com/api/json/v1/1/filter.php?i=" + Userline);
                var response1 = await client1.GetAsync<Meals>(new RestRequest());
                return response1;
            }
            var mealsByIngredient = await GetMealsByIngredient();

            foreach (var ingredientMeal in mealsByIngredient.LsMeals ?? [])
            {
                var tId = ingredientMeal.Id;

                async Task<Meals> GetMealById()
                {
                    var client = new RestClient("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + tId);
                    var response = await client.GetAsync<Meals>(new RestRequest());

                    return response;
                }

                var meal = (await GetMealById()).LsMeals.FirstOrDefault();
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Name: " + meal.Gericht + "\n");

                var propVegan = meal.Vegane_Variante;
                if (!string.IsNullOrWhiteSpace(propVegan))
                    Console.WriteLine("Vegan Alternative? " + meal.Vegane_Variante);

                var propTag = meal.Tag;
                if (propTag != null)
                    Console.WriteLine("Tags: " + meal.Tag);

                var propHerkunft = meal.Herkunft;
                if (!string.IsNullOrWhiteSpace(propHerkunft))
                    Console.WriteLine("Origin: " + meal.Herkunft);

                var propBeschreibung = meal.Beschreibung;
                if (!string.IsNullOrWhiteSpace(propBeschreibung))
                    Console.WriteLine("Discription: " + meal.Beschreibung);

                Console.WriteLine("-- Ingredients:");

                foreach (var property in meal.GetType().GetProperties())
                {
                    var propName = property.Name;
                    var propValue = meal.GetType().GetProperty(propName)?.GetValue(meal)?.ToString();

                    if (propName.Contains("Zutat") && !string.IsNullOrWhiteSpace(propValue))
                    {
                        if (int.TryParse(propName.Replace("Zutat", string.Empty), out int index))
                        {
                            var quantity = meal.GetType().GetProperty("Anzahl" + index)?.GetValue(meal)?.ToString();
                            Console.Write(quantity + " " + propValue + "\n");
                        }
                    }
                }
                Console.WriteLine("\n-----STEP BY STEP INSTRUCTIONS-----");
                var propAnleitung = meal.Anleitung;
                if (!string.IsNullOrWhiteSpace(propAnleitung))
                    Console.WriteLine(meal.Anleitung + "\n");
            }
        }
        else if (Userline == "Back")
        {
            continue;
        }
    }

    //CATEGORY START
    else if (Userinput == "Category" || currentMode == "category")
    {

        //Console.Clear();
        Userinput = null;
        Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Write a category for a dish you want to make. Type L for a list of them (or type Back if you want to search with another filter).");
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------");
        string UserInput = Console.ReadLine();
        if (UserInput != "Back")
        {
            currentMode = "category";
            if (UserInput == "L")
            {
                Console.WriteLine("\n" + "Beef, " + "Breakfast, " + "Chicken, " + "Dessert, " + "Goat, " + "Lamb, " + "Miscellaneous, " + "Pasta, " + "Pork, " + "Seafood, " + "Side, " + "Starter, " + "Vegan, " + "Vegetarian, ");
                UserInput = Console.ReadLine();
            }
            async Task<Meals> GetMealsByCategory()
            {
                var client1 = new RestClient("https://www.themealdb.com/api/json/v1/1/filter.php?c=" + UserInput);
                var response1 = await client1.GetAsync<Meals>(new RestRequest());
                return response1;
            }
            var mealsByCategory = await GetMealsByCategory();

            foreach (var CategoryMeal in mealsByCategory.LsMeals ?? [])
            {
                var tId = CategoryMeal.Id;

                async Task<Meals> GetMealById()
                {
                    var client = new RestClient("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + tId);
                    var response = await client.GetAsync<Meals>(new RestRequest());

                    return response;
                }

                var meal = (await GetMealById()).LsMeals.FirstOrDefault();
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Name: " + meal.Gericht + "\n");

                var propVegan = meal.Vegane_Variante;
                if (!string.IsNullOrWhiteSpace(propVegan))
                    Console.WriteLine("Vegan Alternative? " + meal.Vegane_Variante);

                var propTag = meal.Tag;
                if (propTag != null)
                    Console.WriteLine("Tags: " + meal.Tag);

                var propHerkunft = meal.Herkunft;
                if (!string.IsNullOrWhiteSpace(propHerkunft))
                    Console.WriteLine("Origin: " + meal.Herkunft);

                var propBeschreibung = meal.Beschreibung;
                if (!string.IsNullOrWhiteSpace(propBeschreibung))
                    Console.WriteLine("Discription: " + meal.Beschreibung);

                Console.WriteLine("-- Ingredients:");

                foreach (var property in meal.GetType().GetProperties())
                {
                    var propName = property.Name;
                    var propValue = meal.GetType().GetProperty(propName)?.GetValue(meal)?.ToString();

                    if (propName.Contains("Zutat") && !string.IsNullOrWhiteSpace(propValue))
                    {
                        if (int.TryParse(propName.Replace("Zutat", string.Empty), out int index))
                        {
                            var quantity = meal.GetType().GetProperty("Anzahl" + index)?.GetValue(meal)?.ToString();
                            Console.Write(quantity + " " + propValue + "\n");
                        }
                    }
                }
                Console.WriteLine("\n-----STEP BY STEP INSTRUCTIONS-----");
                var propAnleitung = meal.Anleitung;
                if (!string.IsNullOrWhiteSpace(propAnleitung))
                    Console.WriteLine(meal.Anleitung + "\n");
            }
        }
        if (UserInput == "Back")
        {
            currentMode = null;
            continue;
        }
    }

    //AREA START
    else if (Userinput == "Area")
    {

        Console.Clear();
        Userinput = null;
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Write the name of a Country you want the dish to be from or type Back if you want to search with another filter.");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        string Userline = Console.ReadLine();
        if (Userline != "Back")
        {
            async Task<Meals> GetMealsByOrigin()
            {
                var client1 = new RestClient("https://www.themealdb.com/api/json/v1/1/filter.php?a=" + Userline);
                var response1 = await client1.GetAsync<Meals>(new RestRequest());
                return response1;
            }
            var mealsByOrigin = await GetMealsByOrigin();

            foreach (var areaMeal in mealsByOrigin.LsMeals ?? [])
            {
                var tId = areaMeal.Id;

                async Task<Meals> GetMealById()
                {
                    var client = new RestClient("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + tId);
                    var response = await client.GetAsync<Meals>(new RestRequest());

                    return response;
                }

                var meal = (await GetMealById()).LsMeals.FirstOrDefault();
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Name: " + meal.Gericht + "\n");

                var propVegan = meal.Vegane_Variante;
                if (!string.IsNullOrWhiteSpace(propVegan))
                    Console.WriteLine("Vegan Alternative? " + meal.Vegane_Variante);

                var propTag = meal.Tag;
                if (propTag != null)
                    Console.WriteLine("Tags: " + meal.Tag);

                var propHerkunft = meal.Herkunft;
                if (!string.IsNullOrWhiteSpace(propHerkunft))
                    Console.WriteLine("Origin: " + meal.Herkunft);

                var propBeschreibung = meal.Beschreibung;
                if (!string.IsNullOrWhiteSpace(propBeschreibung))
                    Console.WriteLine("Discription: " + meal.Beschreibung);

                Console.WriteLine("-- Ingredients:");

                foreach (var property in meal.GetType().GetProperties())
                {
                    var propName = property.Name;
                    var propValue = meal.GetType().GetProperty(propName)?.GetValue(meal)?.ToString();

                    if (propName.Contains("Zutat") && !string.IsNullOrWhiteSpace(propValue))
                    {
                        if (int.TryParse(propName.Replace("Zutat", string.Empty), out int index))
                        {
                            var quantity = meal.GetType().GetProperty("Anzahl" + index)?.GetValue(meal)?.ToString();
                            Console.Write(quantity + " " + propValue + "\n");
                        }
                    }

                }
                Console.WriteLine("\n-----STEP BY STEP INSTRUCTIONS-----");
                var propAnleitung = meal.Anleitung;
                if (!string.IsNullOrWhiteSpace(propAnleitung))
                    Console.WriteLine(meal.Anleitung + "\n");
            }
        }
        else if (Userline == "Back")
        {
            continue;
        }
    }

    //RANDOM START
    else if (Userinput == "Surprise Me" || Userinput == "SM") 
    {
        Console.Clear();
        Userinput = null;

        async Task<Meals> GetMealsByName()
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/random.php");
            var response = await client.GetAsync<Meals>(new RestRequest());

            return response;

        }

        var data = await GetMealsByName();

        Console.WriteLine("Count: " + (data.LsMeals != null ? data.LsMeals?.Count : "0"));

        foreach (var meal in data.LsMeals ?? [])
        {

            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Name: " + meal.Gericht + "\n");

            var propVegan = meal.Vegane_Variante;
            if (!string.IsNullOrWhiteSpace(propVegan))
                Console.WriteLine("Vegan Alternative? " + meal.Vegane_Variante);

            var propTag = meal.Tag;
            if (propTag != null)
                Console.WriteLine("Tags: " + meal.Tag);

            var propHerkunft = meal.Herkunft;
            if (!string.IsNullOrWhiteSpace(propHerkunft))
                Console.WriteLine("Origin: " + meal.Herkunft);

            var propBeschreibung = meal.Beschreibung;
            if (!string.IsNullOrWhiteSpace(propBeschreibung))
                Console.WriteLine("Discription: " + meal.Beschreibung);

            Console.WriteLine("\n" + "-- Ingredients:");

            foreach (var property in meal.GetType().GetProperties())
            {
                var propName = property.Name;
                var propValue = meal.GetType().GetProperty(propName)?.GetValue(meal)?.ToString();

                if (propName.Contains("Zutat") && !string.IsNullOrWhiteSpace(propValue))
                {
                    if (int.TryParse(propName.Replace("Zutat", string.Empty), out int index))
                    {
                        var quantity = meal.GetType().GetProperty("Anzahl" + index)?.GetValue(meal)?.ToString();
                        Console.Write(quantity + " " + propValue + "\n");
                    }
                }
            }
            Console.WriteLine("\n-----STEP BY STEP INSTRUCTIONS-----");
            var propAnleitung = meal.Anleitung;
            if (!string.IsNullOrWhiteSpace(propAnleitung))
                Console.WriteLine(meal.Anleitung + "\n");
        }
    }
    else
    {
        continue;
    }
}

public class Meals
{
    [JsonPropertyName("meals")] 
    public List<Meal> LsMeals { get; set; } 
}
public class Meal
{
    [JsonPropertyName("idMeal")]
    public string Id { get; set; }
    [JsonPropertyName("strMeal")]
    public string? Gericht { get; set; } 
    [JsonPropertyName("strMealAlternate")]
    public string Vegane_Variante { get; set; }
    [JsonPropertyName("strCategory")]
    public string Beschreibung { get; set; }
    [JsonPropertyName("strArea")]
    public string Herkunft { get; set; }
    [JsonPropertyName("strInstructions")]
    public string Anleitung { get; set; }
    [JsonPropertyName("strTags")]
    public string Tag { get; set; }
    [JsonPropertyName("strIngredient1")]
    public string Zutat1 { get; set; }
    [JsonPropertyName("strIngredient2")]
    public string Zutat2 { get; set; }
    [JsonPropertyName("strIngredient3")]
    public string Zutat3 { get; set; }
    [JsonPropertyName("strIngredient4")]
    public string Zutat4 { get; set; }
    [JsonPropertyName("strIngredient5")]
    public string Zutat5 { get; set; }
    [JsonPropertyName("strIngredient6")]
    public string Zutat6 { get; set; }
    [JsonPropertyName("strIngredient7")]
    public string Zutat7 { get; set; }
    [JsonPropertyName("strIngredient8")]
    public string Zutat8 { get; set; }
    [JsonPropertyName("strIngredient9")]
    public string Zutat9 { get; set; }
    [JsonPropertyName("strIngredient10")]
    public string Zutat10 { get; set; }
    [JsonPropertyName("strIngredient11")]
    public string Zutat11 { get; set; }
    [JsonPropertyName("strIngredient12")]
    public string Zutat12 { get; set; }
    [JsonPropertyName("strIngredient13")]
    public string Zutat13 { get; set; }
    [JsonPropertyName("strIngredient14")]
    public string Zutat14 { get; set; }
    [JsonPropertyName("strIngredient15")]
    public string Zutat15 { get; set; }
    [JsonPropertyName("strIngredient16")]
    public string Zutat16 { get; set; }
    [JsonPropertyName("strIngredient17")]
    public string Zutat17 { get; set; }
    [JsonPropertyName("strIngredient18")]
    public string Zutat18 { get; set; }
    [JsonPropertyName("strIngredient19")]
    public string Zutat19 { get; set; }
    [JsonPropertyName("strIngredient20")]
    public string Zutat20 { get; set; }
    [JsonPropertyName("strMeasure1")]
    public string Anzahl1 { get; set; }
    [JsonPropertyName("strMeasure2")]
    public string Anzahl2 { get; set; }
    [JsonPropertyName("strMeasure3")]
    public string Anzahl3 { get; set; }
    [JsonPropertyName("strMeasure4")]
    public string Anzahl4 { get; set; }
    [JsonPropertyName("strMeasure5")]
    public string Anzahl5 { get; set; }
    [JsonPropertyName("strMeasure6")]
    public string Anzahl6 { get; set; }
    [JsonPropertyName("strMeasure7")]
    public string Anzahl7 { get; set; }
    [JsonPropertyName("strMeasure8")]
    public string Anzahl8 { get; set; }
    [JsonPropertyName("strMeasure9")]
    public string Anzahl9 { get; set; }
    [JsonPropertyName("strMeasure10")]
    public string Anzahl10 { get; set; }
    [JsonPropertyName("strMeasure11")]
    public string Anzahl11 { get; set; }
    [JsonPropertyName("strMeasure12")]
    public string Anzahl12 { get; set; }
    [JsonPropertyName("strMeasure13")]
    public string Anzahl13 { get; set; }
    [JsonPropertyName("strMeasure14")]
    public string Anzahl14 { get; set; }
    [JsonPropertyName("strMeasure15")]
    public string Anzahl15 { get; set; }
    [JsonPropertyName("strMeasure16")]
    public string Anzahl16 { get; set; }
    [JsonPropertyName("strMeasure17")]
    public string Anzahl17 { get; set; }
    [JsonPropertyName("strMeasure18")]
    public string Anzahl18 { get; set; }
    [JsonPropertyName("strMeasure19")]
    public string Anzahl19 { get; set; }
    [JsonPropertyName("strMeasure20")]
    public string Anzahl20 { get; set; }
    [JsonPropertyName("strSource")]
    public string Quelle { get; set; }
    [JsonPropertyName("dateModified")]
    public string Datum { get; set; }
}
