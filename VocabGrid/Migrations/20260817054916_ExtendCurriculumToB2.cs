using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class ExtendCurriculumToB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Kart tablosunun IDENTITY sayacı seed bloğunun bittiği yerden
            // devam ediyor, yani kullanıcıların oluşturduğu kartlar zamanla
            // seed için ayrılan kimlik aralığına giriyor. Bu blok eklenirken
            // tam olarak bu oldu: 1119-1238 aralığı kullanıcı kartlarıyla
            // doluydu ve ekleme birincil anahtar çakışmasıyla (2627) durdu.
            //
            // Sayacı 10000'e çekmek 5121-9999 aralığını ileride eklenecek seed
            // içeriğine ayırıyor; kullanıcı kartları 10000'in üstünden devam
            // eder ve iki alan bir daha karışmaz.
            //
            // Koşul önemli: 10000'den fazla kartı olan bir kurulumda sayacı
            // geriye çekmek var olan kimliklerle çakışma üretirdi.
            migrationBuilder.Sql(@"
                IF IDENT_CURRENT('Vocabularies') < 10000
                    DBCC CHECKIDENT('Vocabularies', RESEED, 10000);");

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "LessonID", "CreatedAt", "Description", "Level", "OrderIndex", "Title" },
                values: new object[,]
                {
                    { 11, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Talking about the sky and the seasons", "B1", 11, "Weather" },
                    { 12, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Naming how you and others feel", "B1", 12, "Feelings" },
                    { 13, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Prices, sizes and paying at the till", "B1", 13, "Shopping" },
                    { 14, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Everyday words for devices and the web", "B1", 14, "Technology" },
                    { 15, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Landscape, plants and the outdoors", "B1", 15, "Nature" },
                    { 16, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Teams, matches and training", "B2", 16, "Sports" },
                    { 17, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Concerts, instruments and galleries", "B2", 17, "Music and Art" },
                    { 18, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "The kitchen, tools and preparing food", "B2", 18, "Cooking" },
                    { 19, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Accounts, bills and budgeting", "B2", 19, "Money and Banking" },
                    { 20, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Letters, conversations and explaining", "B2", 20, "Communication" }
                });

            migrationBuilder.InsertData(
                table: "Vocabularies",
                columns: new[] { "WordID", "AudioUrl", "CreatedAt", "DeckId", "ExampleSentence", "ImageUrl", "Term", "Translation", "UpdatedAt" },
                values: new object[,]
                {
                    { 5001, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The rain started just after lunch.", null, "Rain", "Yağmur", null },
                    { 5002, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The snow covered the whole garden.", null, "Snow", "Kar", null },
                    { 5003, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sun came out in the afternoon.", null, "Sun", "Güneş", null },
                    { 5004, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A dark cloud is coming from the west.", null, "Cloud", "Bulut", null },
                    { 5005, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The wind is strong near the coast.", null, "Wind", "Rüzgâr", null },
                    { 5006, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The storm lasted all night.", null, "Storm", "Fırtına", null },
                    { 5007, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Thick fog closed the airport.", null, "Fog", "Sis", null },
                    { 5008, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The temperature drops quickly at night.", null, "Temperature", "Sıcaklık", null },
                    { 5009, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Take an umbrella, it might rain.", null, "Umbrella", "Şemsiye", null },
                    { 5010, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Spring is my favourite season.", null, "Season", "Mevsim", null },
                    { 5011, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The water is warm enough to swim.", null, "Warm", "Ilık", null },
                    { 5012, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "It was freezing on the mountain.", null, "Freezing", "Dondurucu", null },
                    { 5013, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She looked happy with the result.", null, "Happy", "Mutlu", null },
                    { 5014, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He felt sad after the news.", null, "Sad", "Üzgün", null },
                    { 5015, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Do not answer while you are angry.", null, "Angry", "Kızgın", null },
                    { 5016, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I am too tired to study tonight.", null, "Tired", "Yorgun", null },
                    { 5017, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The child was afraid of the dark.", null, "Afraid", "Korkmuş", null },
                    { 5018, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We were surprised by the question.", null, "Surprised", "Şaşkın", null },
                    { 5019, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "They are excited about the trip.", null, "Excited", "Heyecanlı", null },
                    { 5020, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He got bored during the lecture.", null, "Bored", "Sıkılmış", null },
                    { 5021, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Her parents are proud of her.", null, "Proud", "Gururlu", null },
                    { 5022, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Try to stay calm and breathe.", null, "Calm", "Sakin", null },
                    { 5023, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I am worried about the exam.", null, "Worried", "Endişeli", null },
                    { 5024, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He felt lonely in the new city.", null, "Lonely", "Yalnız", null },
                    { 5025, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The price includes delivery.", null, "Price", "Fiyat", null },
                    { 5026, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Students get a small discount.", null, "Discount", "İndirim", null },
                    { 5027, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Keep the receipt for the return.", null, "Receipt", "Fiş", null },
                    { 5028, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I would rather pay in cash.", null, "Cash", "Nakit", null },
                    { 5029, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Do you have this in a larger size?", null, "Size", "Beden", null },
                    { 5030, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The shop closes at seven.", null, "Shop", "Dükkân", null },
                    { 5031, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The customer asked for a refund.", null, "Customer", "Müşteri", null },
                    { 5032, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "That jacket is too expensive.", null, "Expensive", "Pahalı", null },
                    { 5033, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The market is cheaper than the mall.", null, "Cheap", "Ucuz", null },
                    { 5034, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "They agreed to a full refund.", null, "Refund", "İade", null },
                    { 5035, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "There was a long queue at the till.", null, "Queue", "Kuyruk", null },
                    { 5036, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delivery takes three working days.", null, "Delivery", "Teslimat", null },
                    { 5037, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My computer restarted by itself.", null, "Computer", "Bilgisayar", null },
                    { 5038, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The screen is too bright at night.", null, "Screen", "Ekran", null },
                    { 5039, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This keyboard is very quiet.", null, "Keyboard", "Klavye", null },
                    { 5040, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Never share your password.", null, "Password", "Parola", null },
                    { 5041, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Save the file before you close it.", null, "File", "Dosya", null },
                    { 5042, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I sent you a message this morning.", null, "Message", "Mesaj", null },
                    { 5043, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The battery lasts about a day.", null, "Battery", "Pil", null },
                    { 5044, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The software needs an update.", null, "Software", "Yazılım", null },
                    { 5045, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The network is down again.", null, "Network", "Ağ", null },
                    { 5046, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Download the file and open it.", null, "Download", "İndirmek", null },
                    { 5047, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The update fixed the problem.", null, "Update", "Güncelleme", null },
                    { 5048, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "You can use two devices at once.", null, "Device", "Cihaz", null },
                    { 5049, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "An old tree stands by the gate.", null, "Tree", "Ağaç", null },
                    { 5050, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She picked a flower for her mother.", null, "Flower", "Çiçek", null },
                    { 5051, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The river runs through the town.", null, "River", "Nehir", null },
                    { 5052, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We climbed the mountain in summer.", null, "Mountain", "Dağ", null },
                    { 5053, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sea was calm that morning.", null, "Sea", "Deniz", null },
                    { 5054, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The forest is quiet after the rain.", null, "Forest", "Orman", null },
                    { 5055, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sky turned orange at sunset.", null, "Sky", "Gökyüzü", null },
                    { 5056, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He threw a stone into the water.", null, "Stone", "Taş", null },
                    { 5057, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We swam in the lake all afternoon.", null, "Lake", "Göl", null },
                    { 5058, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The island is an hour away by boat.", null, "Island", "Ada", null },
                    { 5059, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The beach was empty in October.", null, "Beach", "Plaj", null },
                    { 5060, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A narrow road crosses the valley.", null, "Valley", "Vadi", null },
                    { 5061, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Our team plays on Saturday.", null, "Team", "Takım", null },
                    { 5062, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "That player scored twice.", null, "Player", "Oyuncu", null },
                    { 5063, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The match ended in a draw.", null, "Match", "Maç", null },
                    { 5064, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He scored a goal in the last minute.", null, "Goal", "Gol", null },
                    { 5065, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The coach changed the whole plan.", null, "Coach", "Antrenör", null },
                    { 5066, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Training starts at six in the morning.", null, "Training", "Antrenman", null },
                    { 5067, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "What was the final score?", null, "Score", "Skor", null },
                    { 5068, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The stadium was completely full.", null, "Stadium", "Stadyum", null },
                    { 5069, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The referee stopped the game.", null, "Referee", "Hakem", null },
                    { 5070, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "It was an easy victory.", null, "Victory", "Zafer", null },
                    { 5071, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The defeat surprised everyone.", null, "Defeat", "Yenilgi", null },
                    { 5072, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "An injury kept him out for a month.", null, "Injury", "Sakatlık", null },
                    { 5073, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "That song was written in the sixties.", null, "Song", "Şarkı", null },
                    { 5074, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The singer forgot the second verse.", null, "Singer", "Şarkıcı", null },
                    { 5075, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He plays the guitar every evening.", null, "Guitar", "Gitar", null },
                    { 5076, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The concert sold out in an hour.", null, "Concert", "Konser", null },
                    { 5077, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The painting hangs in the hall.", null, "Painting", "Tablo", null },
                    { 5078, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The artist works with old photographs.", null, "Artist", "Sanatçı", null },
                    { 5079, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The museum is free on Mondays.", null, "Museum", "Müze", null },
                    { 5080, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She walked onto the stage slowly.", null, "Stage", "Sahne", null },
                    { 5081, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The rhythm changes in the chorus.", null, "Rhythm", "Ritim", null },
                    { 5082, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The audience stood up and clapped.", null, "Audience", "Seyirci", null },
                    { 5083, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The exhibition closes next week.", null, "Exhibition", "Sergi", null },
                    { 5084, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A stone sculpture stands in the square.", null, "Sculpture", "Heykel", null },
                    { 5085, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This recipe takes twenty minutes.", null, "Recipe", "Tarif", null },
                    { 5086, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Use a sharp knife for the onions.", null, "Knife", "Bıçak", null },
                    { 5087, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Put the bread on a clean plate.", null, "Plate", "Tabak", null },
                    { 5088, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Add two spoons of sugar.", null, "Spoon", "Kaşık", null },
                    { 5089, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The fork fell under the table.", null, "Fork", "Çatal", null },
                    { 5090, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Heat the oven before you start.", null, "Oven", "Fırın", null },
                    { 5091, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Boil the water for five minutes.", null, "Boil", "Kaynatmak", null },
                    { 5092, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fry the eggs in a little oil.", null, "Fry", "Kızartmak", null },
                    { 5093, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The soup has a strong taste.", null, "Taste", "Tat", null },
                    { 5094, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The bread is fresh from this morning.", null, "Fresh", "Taze", null },
                    { 5095, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Everything on the table was delicious.", null, "Delicious", "Lezzetli", null },
                    { 5096, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "One ingredient is missing.", null, "Ingredient", "Malzeme", null },
                    { 5097, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The bank opens at nine.", null, "Bank", "Banka", null },
                    { 5098, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I opened a second account.", null, "Account", "Hesap", null },
                    { 5099, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The salary arrives on the first.", null, "Salary", "Maaş", null },
                    { 5100, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "They took a loan for the house.", null, "Loan", "Kredi", null },
                    { 5101, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Send the invoice by email.", null, "Invoice", "Fatura", null },
                    { 5102, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The payment did not go through.", null, "Payment", "Ödeme", null },
                    { 5103, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She keeps her savings in the bank.", null, "Savings", "Birikim", null },
                    { 5104, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He paid off the last of his debt.", null, "Debt", "Borç", null },
                    { 5105, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The trip is outside our budget.", null, "Budget", "Bütçe", null },
                    { 5106, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Which currency do they use there?", null, "Currency", "Para birimi", null },
                    { 5107, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The tax is included in the price.", null, "Tax", "Vergi", null },
                    { 5108, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The transfer takes one working day.", null, "Transfer", "Havale", null },
                    { 5109, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A letter arrived from the school.", null, "Letter", "Mektup", null },
                    { 5110, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We had a long conversation.", null, "Conversation", "Sohbet", null },
                    { 5111, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Her advice saved me a lot of time.", null, "Advice", "Tavsiye", null },
                    { 5112, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The news spread very quickly.", null, "News", "Haber", null },
                    { 5113, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He told the same story twice.", null, "Story", "Hikâye", null },
                    { 5114, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Everyone gave a different opinion.", null, "Opinion", "Görüş", null },
                    { 5115, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The translation is not quite right.", null, "Translation", "Çeviri", null },
                    { 5116, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "His speech lasted ten minutes.", null, "Speech", "Konuşma", null },
                    { 5117, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Can you explain that again?", null, "Explain", "Açıklamak", null },
                    { 5118, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Please repeat the last sentence.", null, "Repeat", "Tekrarlamak", null },
                    { 5119, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I agree with most of your points.", null, "Agree", "Katılmak", null },
                    { 5120, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "It is fine to disagree politely.", null, "Disagree", "Karşı çıkmak", null }
                });

            migrationBuilder.InsertData(
                table: "LessonVocabularies",
                columns: new[] { "LessonID", "WordID" },
                values: new object[,]
                {
                    { 11, 5001 },
                    { 11, 5002 },
                    { 11, 5003 },
                    { 11, 5004 },
                    { 11, 5005 },
                    { 11, 5006 },
                    { 11, 5007 },
                    { 11, 5008 },
                    { 11, 5009 },
                    { 11, 5010 },
                    { 11, 5011 },
                    { 11, 5012 },
                    { 12, 5013 },
                    { 12, 5014 },
                    { 12, 5015 },
                    { 12, 5016 },
                    { 12, 5017 },
                    { 12, 5018 },
                    { 12, 5019 },
                    { 12, 5020 },
                    { 12, 5021 },
                    { 12, 5022 },
                    { 12, 5023 },
                    { 12, 5024 },
                    { 13, 5025 },
                    { 13, 5026 },
                    { 13, 5027 },
                    { 13, 5028 },
                    { 13, 5029 },
                    { 13, 5030 },
                    { 13, 5031 },
                    { 13, 5032 },
                    { 13, 5033 },
                    { 13, 5034 },
                    { 13, 5035 },
                    { 13, 5036 },
                    { 14, 5037 },
                    { 14, 5038 },
                    { 14, 5039 },
                    { 14, 5040 },
                    { 14, 5041 },
                    { 14, 5042 },
                    { 14, 5043 },
                    { 14, 5044 },
                    { 14, 5045 },
                    { 14, 5046 },
                    { 14, 5047 },
                    { 14, 5048 },
                    { 15, 5049 },
                    { 15, 5050 },
                    { 15, 5051 },
                    { 15, 5052 },
                    { 15, 5053 },
                    { 15, 5054 },
                    { 15, 5055 },
                    { 15, 5056 },
                    { 15, 5057 },
                    { 15, 5058 },
                    { 15, 5059 },
                    { 15, 5060 },
                    { 16, 5061 },
                    { 16, 5062 },
                    { 16, 5063 },
                    { 16, 5064 },
                    { 16, 5065 },
                    { 16, 5066 },
                    { 16, 5067 },
                    { 16, 5068 },
                    { 16, 5069 },
                    { 16, 5070 },
                    { 16, 5071 },
                    { 16, 5072 },
                    { 17, 5073 },
                    { 17, 5074 },
                    { 17, 5075 },
                    { 17, 5076 },
                    { 17, 5077 },
                    { 17, 5078 },
                    { 17, 5079 },
                    { 17, 5080 },
                    { 17, 5081 },
                    { 17, 5082 },
                    { 17, 5083 },
                    { 17, 5084 },
                    { 18, 5085 },
                    { 18, 5086 },
                    { 18, 5087 },
                    { 18, 5088 },
                    { 18, 5089 },
                    { 18, 5090 },
                    { 18, 5091 },
                    { 18, 5092 },
                    { 18, 5093 },
                    { 18, 5094 },
                    { 18, 5095 },
                    { 18, 5096 },
                    { 19, 5097 },
                    { 19, 5098 },
                    { 19, 5099 },
                    { 19, 5100 },
                    { 19, 5101 },
                    { 19, 5102 },
                    { 19, 5103 },
                    { 19, 5104 },
                    { 19, 5105 },
                    { 19, 5106 },
                    { 19, 5107 },
                    { 19, 5108 },
                    { 20, 5109 },
                    { 20, 5110 },
                    { 20, 5111 },
                    { 20, 5112 },
                    { 20, 5113 },
                    { 20, 5114 },
                    { 20, 5115 },
                    { 20, 5116 },
                    { 20, 5117 },
                    { 20, 5118 },
                    { 20, 5119 },
                    { 20, 5120 }
                });

            migrationBuilder.InsertData(
                table: "VocabularyTags",
                columns: new[] { "TagId", "WordID" },
                values: new object[,]
                {
                    { 3, 5001 },
                    { 3, 5002 },
                    { 3, 5003 },
                    { 3, 5004 },
                    { 3, 5005 },
                    { 3, 5006 },
                    { 3, 5007 },
                    { 3, 5008 },
                    { 3, 5009 },
                    { 3, 5010 },
                    { 4, 5011 },
                    { 4, 5012 },
                    { 4, 5013 },
                    { 4, 5014 },
                    { 4, 5015 },
                    { 4, 5016 },
                    { 4, 5017 },
                    { 4, 5018 },
                    { 4, 5019 },
                    { 4, 5020 },
                    { 4, 5021 },
                    { 4, 5022 },
                    { 4, 5023 },
                    { 4, 5024 },
                    { 3, 5025 },
                    { 3, 5026 },
                    { 3, 5027 },
                    { 3, 5028 },
                    { 3, 5029 },
                    { 3, 5030 },
                    { 3, 5031 },
                    { 4, 5032 },
                    { 4, 5033 },
                    { 3, 5034 },
                    { 3, 5035 },
                    { 3, 5036 },
                    { 3, 5037 },
                    { 3, 5038 },
                    { 3, 5039 },
                    { 3, 5040 },
                    { 3, 5041 },
                    { 3, 5042 },
                    { 3, 5043 },
                    { 3, 5044 },
                    { 3, 5045 },
                    { 14, 5046 },
                    { 3, 5047 },
                    { 3, 5048 },
                    { 3, 5049 },
                    { 3, 5050 },
                    { 3, 5051 },
                    { 3, 5052 },
                    { 3, 5053 },
                    { 3, 5054 },
                    { 3, 5055 },
                    { 3, 5056 },
                    { 3, 5057 },
                    { 3, 5058 },
                    { 3, 5059 },
                    { 3, 5060 },
                    { 3, 5061 },
                    { 3, 5062 },
                    { 3, 5063 },
                    { 3, 5064 },
                    { 3, 5065 },
                    { 3, 5066 },
                    { 3, 5067 },
                    { 3, 5068 },
                    { 3, 5069 },
                    { 3, 5070 },
                    { 3, 5071 },
                    { 3, 5072 },
                    { 3, 5073 },
                    { 3, 5074 },
                    { 3, 5075 },
                    { 3, 5076 },
                    { 3, 5077 },
                    { 3, 5078 },
                    { 3, 5079 },
                    { 3, 5080 },
                    { 3, 5081 },
                    { 14, 5081 },
                    { 3, 5082 },
                    { 14, 5082 },
                    { 3, 5083 },
                    { 14, 5083 },
                    { 3, 5084 },
                    { 14, 5084 },
                    { 3, 5085 },
                    { 3, 5086 },
                    { 3, 5087 },
                    { 3, 5088 },
                    { 3, 5089 },
                    { 3, 5090 },
                    { 14, 5091 },
                    { 14, 5092 },
                    { 3, 5093 },
                    { 4, 5094 },
                    { 4, 5095 },
                    { 3, 5096 },
                    { 3, 5097 },
                    { 10, 5097 },
                    { 3, 5098 },
                    { 10, 5098 },
                    { 3, 5099 },
                    { 10, 5099 },
                    { 3, 5100 },
                    { 10, 5100 },
                    { 3, 5101 },
                    { 10, 5101 },
                    { 3, 5102 },
                    { 10, 5102 },
                    { 3, 5103 },
                    { 10, 5103 },
                    { 3, 5104 },
                    { 10, 5104 },
                    { 3, 5105 },
                    { 10, 5105 },
                    { 3, 5106 },
                    { 10, 5106 },
                    { 3, 5107 },
                    { 10, 5107 },
                    { 3, 5108 },
                    { 10, 5108 },
                    { 3, 5109 },
                    { 3, 5110 },
                    { 3, 5111 },
                    { 3, 5112 },
                    { 3, 5113 },
                    { 3, 5114 },
                    { 3, 5115 },
                    { 3, 5116 },
                    { 14, 5117 },
                    { 14, 5118 },
                    { 14, 5119 },
                    { 14, 5120 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5001 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5002 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5003 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5004 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5005 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5006 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5007 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5008 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5009 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5010 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5011 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 11, 5012 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5013 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5014 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5015 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5016 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5017 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5018 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5019 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5020 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5021 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5022 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5023 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 12, 5024 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5025 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5026 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5027 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5028 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5029 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5030 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5031 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5032 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5033 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5034 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5035 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 13, 5036 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5037 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5038 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5039 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5040 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5041 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5042 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5043 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5044 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5045 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5046 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5047 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 14, 5048 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5049 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5050 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5051 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5052 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5053 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5054 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5055 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5056 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5057 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5058 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5059 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 15, 5060 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5061 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5062 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5063 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5064 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5065 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5066 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5067 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5068 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5069 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5070 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5071 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 16, 5072 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5073 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5074 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5075 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5076 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5077 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5078 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5079 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5080 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5081 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5082 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5083 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 17, 5084 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5085 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5086 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5087 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5088 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5089 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5090 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5091 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5092 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5093 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5094 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5095 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 18, 5096 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5097 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5098 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5099 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5100 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5101 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5102 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5103 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5104 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5105 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5106 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5107 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 19, 5108 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5109 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5110 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5111 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5112 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5113 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5114 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5115 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5116 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5117 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5118 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5119 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 20, 5120 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5001 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5002 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5003 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5004 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5005 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5006 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5007 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5008 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5009 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5010 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5011 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5012 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5013 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5014 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5015 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5016 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5017 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5018 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5019 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5020 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5021 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5022 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5023 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5024 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5025 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5026 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5027 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5028 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5029 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5030 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5031 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5032 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5033 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5034 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5035 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5036 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5037 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5038 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5039 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5040 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5041 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5042 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5043 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5044 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5045 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5046 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5047 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5048 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5049 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5050 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5051 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5052 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5053 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5054 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5055 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5056 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5057 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5058 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5059 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5060 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5061 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5062 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5063 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5064 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5065 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5066 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5067 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5068 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5069 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5070 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5071 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5072 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5073 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5074 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5075 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5076 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5077 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5078 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5079 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5080 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5081 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5081 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5082 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5082 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5083 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5083 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5084 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5084 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5085 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5086 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5087 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5088 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5089 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5090 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5091 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5092 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5093 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5094 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 4, 5095 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5096 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5097 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5097 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5098 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5098 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5099 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5099 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5100 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5100 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5101 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5101 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5102 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5102 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5103 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5103 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5104 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5104 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5105 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5105 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5106 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5106 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5107 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5107 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5108 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 10, 5108 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5109 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5110 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5111 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5112 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5113 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5114 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5115 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 3, 5116 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5117 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5118 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5119 });

            migrationBuilder.DeleteData(
                table: "VocabularyTags",
                keyColumns: new[] { "TagId", "WordID" },
                keyValues: new object[] { 14, 5120 });

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5001);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5002);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5003);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5004);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5005);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5006);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5007);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5008);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5009);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5010);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5011);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5012);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5013);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5014);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5015);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5016);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5017);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5018);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5019);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5020);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5021);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5022);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5023);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5024);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5025);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5026);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5027);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5028);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5029);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5030);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5031);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5032);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5033);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5034);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5035);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5036);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5037);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5038);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5039);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5040);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5041);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5042);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5043);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5044);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5045);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5046);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5047);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5048);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5049);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5050);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5051);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5052);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5053);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5054);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5055);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5056);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5057);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5058);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5059);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5060);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5061);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5062);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5063);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5064);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5065);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5066);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5067);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5068);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5069);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5070);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5071);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5072);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5073);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5074);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5075);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5076);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5077);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5078);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5079);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5080);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5081);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5082);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5083);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5084);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5085);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5086);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5087);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5088);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5089);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5090);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5091);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5092);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5093);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5094);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5095);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5096);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5097);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5098);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5099);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5100);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5101);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5102);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5103);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5104);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5105);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5106);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5107);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5108);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5109);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5110);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5111);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5112);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5113);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5114);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5115);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5116);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5117);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5118);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5119);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 5120);
        }
    }
}
