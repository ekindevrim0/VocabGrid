using Microsoft.EntityFrameworkCore;
using VocabGrid.Entities;

namespace VocabGrid.Data;

/// <summary>
/// Müfredatın orta seviye bölümü: 11-20. dersler ve 120 kelime.
///
/// <see cref="CurriculumSeedData"/> A1-A2 içeriğini taşıyor; bu dosya B1-B2
/// devamını. Ayrı durmalarının nedeni yalnızca dosya boyutu değil: seviyeler
/// bağımsız olarak genişletiliyor ve bir seviyeye kelime eklerken diğerinin
/// kimlik aralığına dokunmamak gerekiyor.
///
/// Kelime kimlikleri 5001'den başlıyor. Bitişik olan 1119 seçilemezdi: kart
/// tablosunun IDENTITY sayacı seed bloğunun bittiği yerden devam ettiği için
/// kullanıcıların oluşturduğu kartlar çoktan 1119 ve üstünü doldurmuştu — bu
/// blok oraya konsaydı var olan kurulumlarda birincil anahtar çakışırdı.
/// (Gerçekten çakıştı; migration 2627 ile durdu.)
///
/// Bu yüzden blok 5001-5120 aralığında ve migration, sayacı 10000'e çekerek
/// aradaki boşluğu ileride eklenecek seed içeriğine ayırıyor. Yeni bir seviye
/// eklerken kimlikleri 5121'den sürdürün; 10000 sınırına yaklaşıldığında
/// sayacı yeniden yukarı taşımak gerekir.
///
/// Kimlikler sabit yazılı; sırayla üretilseydi araya tek bir kelime eklemek
/// sonraki tüm kayıtların kimliğini kaydırır ve var olan kurulumlarda
/// kullanıcıların ilerlemesi yanlış kelimeye bağlanırdı.
/// </summary>
internal static class CurriculumSeedDataB1
{
    private static readonly DateTime SeedCreatedAt = new(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc);

    private static readonly (int Id, string Title, string Description, string Level, int Order)[] Lessons =
    {
        (11, "Weather",             "Talking about the sky and the seasons",     "B1", 11),
        (12, "Feelings",            "Naming how you and others feel",            "B1", 12),
        (13, "Shopping",            "Prices, sizes and paying at the till",      "B1", 13),
        (14, "Technology",          "Everyday words for devices and the web",    "B1", 14),
        (15, "Nature",              "Landscape, plants and the outdoors",        "B1", 15),
        (16, "Sports",              "Teams, matches and training",               "B2", 16),
        (17, "Music and Art",       "Concerts, instruments and galleries",       "B2", 17),
        (18, "Cooking",             "The kitchen, tools and preparing food",     "B2", 18),
        (19, "Money and Banking",   "Accounts, bills and budgeting",             "B2", 19),
        (20, "Communication",       "Letters, conversations and explaining",     "B2", 20),
    };

    private static readonly (int WordId, int LessonId, string Term, string Translation, string Example)[] Words =
    {
        // --- Ders 11: Weather ---
        (5001, 11, "Rain",         "Yağmur",        "The rain started just after lunch."),
        (5002, 11, "Snow",         "Kar",           "The snow covered the whole garden."),
        (5003, 11, "Sun",          "Güneş",         "The sun came out in the afternoon."),
        (5004, 11, "Cloud",        "Bulut",         "A dark cloud is coming from the west."),
        (5005, 11, "Wind",         "Rüzgâr",        "The wind is strong near the coast."),
        (5006, 11, "Storm",        "Fırtına",       "The storm lasted all night."),
        (5007, 11, "Fog",          "Sis",           "Thick fog closed the airport."),
        (5008, 11, "Temperature",  "Sıcaklık",      "The temperature drops quickly at night."),
        (5009, 11, "Umbrella",     "Şemsiye",       "Take an umbrella, it might rain."),
        (5010, 11, "Season",       "Mevsim",        "Spring is my favourite season."),
        (5011, 11, "Warm",         "Ilık",          "The water is warm enough to swim."),
        (5012, 11, "Freezing",     "Dondurucu",     "It was freezing on the mountain."),

        // --- Ders 12: Feelings ---
        (5013, 12, "Happy",        "Mutlu",         "She looked happy with the result."),
        (5014, 12, "Sad",          "Üzgün",         "He felt sad after the news."),
        (5015, 12, "Angry",        "Kızgın",        "Do not answer while you are angry."),
        (5016, 12, "Tired",        "Yorgun",        "I am too tired to study tonight."),
        (5017, 12, "Afraid",       "Korkmuş",       "The child was afraid of the dark."),
        (5018, 12, "Surprised",    "Şaşkın",        "We were surprised by the question."),
        (5019, 12, "Excited",      "Heyecanlı",     "They are excited about the trip."),
        (5020, 12, "Bored",        "Sıkılmış",      "He got bored during the lecture."),
        (5021, 12, "Proud",        "Gururlu",       "Her parents are proud of her."),
        (5022, 12, "Calm",         "Sakin",         "Try to stay calm and breathe."),
        (5023, 12, "Worried",      "Endişeli",      "I am worried about the exam."),
        (5024, 12, "Lonely",       "Yalnız",        "He felt lonely in the new city."),

        // --- Ders 13: Shopping ---
        (5025, 13, "Price",        "Fiyat",         "The price includes delivery."),
        (5026, 13, "Discount",     "İndirim",       "Students get a small discount."),
        (5027, 13, "Receipt",      "Fiş",           "Keep the receipt for the return."),
        (5028, 13, "Cash",         "Nakit",         "I would rather pay in cash."),
        (5029, 13, "Size",         "Beden",         "Do you have this in a larger size?"),
        (5030, 13, "Shop",         "Dükkân",        "The shop closes at seven."),
        (5031, 13, "Customer",     "Müşteri",       "The customer asked for a refund."),
        (5032, 13, "Expensive",    "Pahalı",        "That jacket is too expensive."),
        (5033, 13, "Cheap",        "Ucuz",          "The market is cheaper than the mall."),
        (5034, 13, "Refund",       "İade",          "They agreed to a full refund."),
        (5035, 13, "Queue",        "Kuyruk",        "There was a long queue at the till."),
        (5036, 13, "Delivery",     "Teslimat",      "Delivery takes three working days."),

        // --- Ders 14: Technology ---
        (5037, 14, "Computer",     "Bilgisayar",    "My computer restarted by itself."),
        (5038, 14, "Screen",       "Ekran",         "The screen is too bright at night."),
        (5039, 14, "Keyboard",     "Klavye",        "This keyboard is very quiet."),
        (5040, 14, "Password",     "Parola",        "Never share your password."),
        (5041, 14, "File",         "Dosya",         "Save the file before you close it."),
        (5042, 14, "Message",      "Mesaj",         "I sent you a message this morning."),
        (5043, 14, "Battery",      "Pil",           "The battery lasts about a day."),
        (5044, 14, "Software",     "Yazılım",       "The software needs an update."),
        (5045, 14, "Network",      "Ağ",            "The network is down again."),
        (5046, 14, "Download",     "İndirmek",      "Download the file and open it."),
        (5047, 14, "Update",       "Güncelleme",    "The update fixed the problem."),
        (5048, 14, "Device",       "Cihaz",         "You can use two devices at once."),

        // --- Ders 15: Nature ---
        (5049, 15, "Tree",         "Ağaç",          "An old tree stands by the gate."),
        (5050, 15, "Flower",       "Çiçek",         "She picked a flower for her mother."),
        (5051, 15, "River",        "Nehir",         "The river runs through the town."),
        (5052, 15, "Mountain",     "Dağ",           "We climbed the mountain in summer."),
        (5053, 15, "Sea",          "Deniz",         "The sea was calm that morning."),
        (5054, 15, "Forest",       "Orman",         "The forest is quiet after the rain."),
        (5055, 15, "Sky",          "Gökyüzü",       "The sky turned orange at sunset."),
        (5056, 15, "Stone",        "Taş",           "He threw a stone into the water."),
        (5057, 15, "Lake",         "Göl",           "We swam in the lake all afternoon."),
        (5058, 15, "Island",       "Ada",           "The island is an hour away by boat."),
        (5059, 15, "Beach",        "Plaj",          "The beach was empty in October."),
        (5060, 15, "Valley",       "Vadi",          "A narrow road crosses the valley."),

        // --- Ders 16: Sports ---
        (5061, 16, "Team",         "Takım",         "Our team plays on Saturday."),
        (5062, 16, "Player",       "Oyuncu",        "That player scored twice."),
        (5063, 16, "Match",        "Maç",           "The match ended in a draw."),
        (5064, 16, "Goal",         "Gol",           "He scored a goal in the last minute."),
        (5065, 16, "Coach",        "Antrenör",      "The coach changed the whole plan."),
        (5066, 16, "Training",     "Antrenman",     "Training starts at six in the morning."),
        (5067, 16, "Score",        "Skor",          "What was the final score?"),
        (5068, 16, "Stadium",      "Stadyum",       "The stadium was completely full."),
        (5069, 16, "Referee",      "Hakem",         "The referee stopped the game."),
        (5070, 16, "Victory",      "Zafer",         "It was an easy victory."),
        (5071, 16, "Defeat",       "Yenilgi",       "The defeat surprised everyone."),
        (5072, 16, "Injury",       "Sakatlık",      "An injury kept him out for a month."),

        // --- Ders 17: Music and Art ---
        (5073, 17, "Song",         "Şarkı",         "That song was written in the sixties."),
        (5074, 17, "Singer",       "Şarkıcı",       "The singer forgot the second verse."),
        (5075, 17, "Guitar",       "Gitar",         "He plays the guitar every evening."),
        (5076, 17, "Concert",      "Konser",        "The concert sold out in an hour."),
        (5077, 17, "Painting",     "Tablo",         "The painting hangs in the hall."),
        (5078, 17, "Artist",       "Sanatçı",       "The artist works with old photographs."),
        (5079, 17, "Museum",       "Müze",          "The museum is free on Mondays."),
        (5080, 17, "Stage",        "Sahne",         "She walked onto the stage slowly."),
        (5081, 17, "Rhythm",       "Ritim",         "The rhythm changes in the chorus."),
        (5082, 17, "Audience",     "Seyirci",       "The audience stood up and clapped."),
        (5083, 17, "Exhibition",   "Sergi",         "The exhibition closes next week."),
        (5084, 17, "Sculpture",    "Heykel",        "A stone sculpture stands in the square."),

        // --- Ders 18: Cooking ---
        (5085, 18, "Recipe",       "Tarif",         "This recipe takes twenty minutes."),
        (5086, 18, "Knife",        "Bıçak",         "Use a sharp knife for the onions."),
        (5087, 18, "Plate",        "Tabak",         "Put the bread on a clean plate."),
        (5088, 18, "Spoon",        "Kaşık",         "Add two spoons of sugar."),
        (5089, 18, "Fork",         "Çatal",         "The fork fell under the table."),
        (5090, 18, "Oven",         "Fırın",         "Heat the oven before you start."),
        (5091, 18, "Boil",         "Kaynatmak",     "Boil the water for five minutes."),
        (5092, 18, "Fry",          "Kızartmak",     "Fry the eggs in a little oil."),
        (5093, 18, "Taste",        "Tat",           "The soup has a strong taste."),
        (5094, 18, "Fresh",        "Taze",          "The bread is fresh from this morning."),
        (5095, 18, "Delicious",    "Lezzetli",      "Everything on the table was delicious."),
        (5096, 18, "Ingredient",   "Malzeme",       "One ingredient is missing."),

        // --- Ders 19: Money and Banking ---
        (5097, 19, "Bank",         "Banka",         "The bank opens at nine."),
        (5098, 19, "Account",      "Hesap",         "I opened a second account."),
        (5099, 19, "Salary",       "Maaş",          "The salary arrives on the first."),
        (5100, 19, "Loan",         "Kredi",         "They took a loan for the house."),
        (5101, 19, "Invoice",      "Fatura",        "Send the invoice by email."),
        (5102, 19, "Payment",      "Ödeme",         "The payment did not go through."),
        (5103, 19, "Savings",      "Birikim",       "She keeps her savings in the bank."),
        (5104, 19, "Debt",         "Borç",          "He paid off the last of his debt."),
        (5105, 19, "Budget",       "Bütçe",         "The trip is outside our budget."),
        (5106, 19, "Currency",     "Para birimi",   "Which currency do they use there?"),
        (5107, 19, "Tax",          "Vergi",         "The tax is included in the price."),
        (5108, 19, "Transfer",     "Havale",        "The transfer takes one working day."),

        // --- Ders 20: Communication ---
        (5109, 20, "Letter",       "Mektup",        "A letter arrived from the school."),
        (5110, 20, "Conversation", "Sohbet",        "We had a long conversation."),
        (5111, 20, "Advice",       "Tavsiye",       "Her advice saved me a lot of time."),
        (5112, 20, "News",         "Haber",         "The news spread very quickly."),
        (5113, 20, "Story",        "Hikâye",        "He told the same story twice."),
        (5114, 20, "Opinion",      "Görüş",         "Everyone gave a different opinion."),
        (5115, 20, "Translation",  "Çeviri",        "The translation is not quite right."),
        (5116, 20, "Speech",       "Konuşma",       "His speech lasted ten minutes."),
        (5117, 20, "Explain",      "Açıklamak",     "Can you explain that again?"),
        (5118, 20, "Repeat",       "Tekrarlamak",   "Please repeat the last sentence."),
        (5119, 20, "Agree",        "Katılmak",      "I agree with most of your points."),
        (5120, 20, "Disagree",     "Karşı çıkmak",  "It is fine to disagree politely."),
    };

    internal static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lesson>().HasData(Lessons.Select(lesson => new Lesson
        {
            LessonID = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            Level = lesson.Level,
            OrderIndex = lesson.Order,
            CreatedAt = SeedCreatedAt
        }));

        modelBuilder.Entity<Vocabulary>().HasData(Words.Select(word => new Vocabulary
        {
            WordID = word.WordId,
            // DeckId null: bu kelimeler kimsenin destesine ait değil, derse
            // bağlı oldukları için herkesin tekrar kuyruğuna girerler.
            DeckId = null,
            Term = word.Term,
            Translation = word.Translation,
            ExampleSentence = word.Example,
            CreatedAt = SeedCreatedAt
        }));

        modelBuilder.Entity<LessonVocabulary>().HasData(Words.Select(word => new LessonVocabulary
        {
            LessonID = word.LessonId,
            WordID = word.WordId
        }));

        ApplyTags(modelBuilder);
    }

    /// <summary>
    /// Etiketler yine aralık aralık, çünkü her ders kendi içinde tek türden.
    /// "High frequency" bu blokta hiç kullanılmıyor: B1-B2 içeriği tanım gereği
    /// günlük dilde daha seyrek geçer. Sözcük türü aralıkları kelime listesi
    /// okunarak çıkarıldı — 11. derste son iki kelime sıfat, 14 ve 18. derste
    /// fiiller ayrı aralıkta.
    /// </summary>
    private static void ApplyTags(ModelBuilder modelBuilder)
    {
        const int Noun = 3;
        const int Adjective = 4;
        const int Academic = 10;
        const int Advanced = 14;

        var links = new List<VocabularyTag>();

        void Link(int firstWordId, int lastWordId, params int[] tagIds)
        {
            for (var wordId = firstWordId; wordId <= lastWordId; wordId++)
            {
                foreach (var tagId in tagIds)
                {
                    links.Add(new VocabularyTag { WordID = wordId, TagId = tagId });
                }
            }
        }

        Link(5001, 5010, Noun);                  // Hava olayları — isimler
        Link(5011, 5012, Adjective);             // warm, freezing
        Link(5013, 5024, Adjective);             // Duygular — hepsi sıfat
        Link(5025, 5031, Noun);                  // Alışveriş — isimler
        Link(5032, 5033, Adjective);             // expensive, cheap
        Link(5034, 5036, Noun);                  // refund, queue, delivery
        Link(5037, 5045, Noun);                  // Teknoloji — isimler
        Link(5046, 5046, Advanced);              // download — fiil, teknik
        Link(5047, 5048, Noun);                  // update, device
        Link(5049, 5060, Noun);                  // Doğa — isimler
        Link(5061, 5072, Noun);                  // Spor — isimler
        Link(5073, 5080, Noun);                  // Müzik ve sanat — isimler
        Link(5081, 5084, Noun, Advanced);        // rhythm, audience, exhibition, sculpture
        Link(5085, 5090, Noun);                  // Mutfak araçları
        Link(5091, 5092, Advanced);              // boil, fry — fiil
        Link(5093, 5093, Noun);                  // taste
        Link(5094, 5095, Adjective);             // fresh, delicious
        Link(5096, 5096, Noun);                  // ingredient
        Link(5097, 5108, Noun, Academic);        // Bankacılık — resmî/akademik alan
        Link(5109, 5116, Noun);                  // İletişim — isimler
        Link(5117, 5120, Advanced);              // explain, repeat, agree, disagree — fiil

        modelBuilder.Entity<VocabularyTag>().HasData(links);
    }
}
