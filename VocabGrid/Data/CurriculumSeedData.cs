using Microsoft.EntityFrameworkCore;
using VocabGrid.Entities;

namespace VocabGrid.Data;

/// <summary>
/// Paylaşılan İngilizce → Türkçe müfredatı: dersler ve arkalarındaki
/// deste-siz (<c>DeckId == null</c>) <see cref="Vocabulary"/> kayıtları.
///
/// Bu kelimeler bir kullanıcıya ait değildir; <see cref="LessonVocabulary"/>
/// üzerinden bir derse bağlı oldukları için herkesin tekrar kuyruğuna girerler
/// (bkz. ProgressController.GetDueReviews). Kullanıcının kendi destesindeki
/// kartlardan farkı budur.
///
/// Ayrı bir dosyada duruyor çünkü OnModelCreating zaten 400 satır; bu kadar
/// içeriği oraya gömmek okunmaz hale getirirdi.
/// </summary>
internal static class CurriculumSeedData
{
    private static readonly DateTime SeedCreatedAt = new(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Ders kimlikleri 1 ve 2 zaten kullanımda (Greetings, Food Basics), bu
    /// yüzden yeni dersler 3'ten başlar ve o iki derse de kelime bağlanır —
    /// şimdiye kadar quiz'leri vardı ama tek bir kelimeleri yoktu.
    /// </summary>
    private static readonly (int Id, string Title, string Description, string Level, int Order)[] Lessons =
    {
        (3,  "Numbers",            "Counting from one to a thousand",       "A1", 3),
        (4,  "Time and Days",      "Days of the week and talking about when", "A1", 4),
        (5,  "Family",             "People closest to you",                 "A1", 5),
        (6,  "Colours",            "The colours you need every day",        "A1", 6),
        (7,  "Travel and Directions", "Getting around and finding your way", "A2", 7),
        (8,  "Work and School",    "Words for the office and the classroom", "A2", 8),
        (9,  "Body and Health",    "Describing how you feel and seeing a doctor", "A2", 9),
        (10, "Home and Daily Life", "Around the house and the everyday",     "A2", 10),
    };

    /// <summary>
    /// Kelime kimlikleri bilinçli olarak 1000'in üzerinden başlar. Bu tablo
    /// kimliği kullanıcıların oluşturduğu kartlarla paylaşır; düşük numaralar
    /// zaten dolu olabileceği için seed'i oraya koymak var olan kurulumlarda
    /// çakışırdı. Kimlikler sabit yazılmıştır — sıraya göre üretilseydi araya
    /// tek bir kelime eklemek sonraki tüm kayıtların kimliğini kaydırırdı.
    /// </summary>
    private static readonly (int WordId, int LessonId, string Term, string Translation, string Example)[] Words =
    {
        // --- Ders 1: Greetings ---
        (1001, 1, "Hello", "Merhaba", "Hello, nice to meet you."),
        (1002, 1, "Good morning", "Günaydın", "Good morning, did you sleep well?"),
        (1003, 1, "Good evening", "İyi akşamlar", "Good evening, please come in."),
        (1004, 1, "Good night", "İyi geceler", "Good night, see you tomorrow."),
        (1005, 1, "Goodbye", "Hoşça kal", "Goodbye, take care of yourself."),
        (1006, 1, "Thank you", "Teşekkürler", "Thank you for all your help."),
        (1007, 1, "Please", "Lütfen", "Please wait here for a moment."),
        (1008, 1, "Sorry", "Özür dilerim", "Sorry, I am running late."),
        (1009, 1, "Excuse me", "Affedersiniz", "Excuse me, where is the station?"),
        (1010, 1, "Yes", "Evet", "Yes, that sounds good to me."),
        (1011, 1, "No", "Hayır", "No, thank you very much."),
        (1012, 1, "Welcome", "Hoş geldiniz", "Welcome to our home."),

        // --- Ders 2: Food Basics ---
        (1013, 2, "Water", "Su", "Could I have a glass of water?"),
        (1014, 2, "Bread", "Ekmek", "The bread is still warm."),
        (1015, 2, "Milk", "Süt", "She drinks milk every morning."),
        (1016, 2, "Tea", "Çay", "Would you like a cup of tea?"),
        (1017, 2, "Coffee", "Kahve", "I drink coffee after lunch."),
        (1018, 2, "Apple", "Elma", "This apple is very sweet."),
        (1019, 2, "Cheese", "Peynir", "We bought cheese and olives."),
        (1020, 2, "Egg", "Yumurta", "He ate one egg for breakfast."),
        (1021, 2, "Rice", "Pirinç", "The rice is cooked with butter."),
        (1022, 2, "Meat", "Et", "The meat is on the grill."),
        (1023, 2, "Salt", "Tuz", "Please pass me the salt."),
        (1024, 2, "Sugar", "Şeker", "No sugar in my coffee, thanks."),

        // --- Ders 3: Numbers ---
        (1025, 3, "One", "Bir", "I only need one ticket."),
        (1026, 3, "Two", "İki", "We waited two hours."),
        (1027, 3, "Three", "Üç", "There are three rooms upstairs."),
        (1028, 3, "Four", "Dört", "The table seats four people."),
        (1029, 3, "Five", "Beş", "The shop opens at five."),
        (1030, 3, "Six", "Altı", "She has six brothers and sisters."),
        (1031, 3, "Seven", "Yedi", "The week has seven days."),
        (1032, 3, "Eight", "Sekiz", "He works eight hours a day."),
        (1033, 3, "Nine", "Dokuz", "The train leaves at nine."),
        (1034, 3, "Ten", "On", "Give me ten minutes."),
        (1035, 3, "Hundred", "Yüz", "The book has a hundred pages."),
        (1036, 3, "Thousand", "Bin", "A thousand people came."),

        // --- Ders 4: Time and Days ---
        (1037, 4, "Today", "Bugün", "Today is a busy day."),
        (1038, 4, "Tomorrow", "Yarın", "We will finish it tomorrow."),
        (1039, 4, "Yesterday", "Dün", "Yesterday it rained all day."),
        (1040, 4, "Monday", "Pazartesi", "The course starts on Monday."),
        (1041, 4, "Tuesday", "Salı", "I have a meeting on Tuesday."),
        (1042, 4, "Wednesday", "Çarşamba", "Wednesday is my day off."),
        (1043, 4, "Thursday", "Perşembe", "We travel on Thursday."),
        (1044, 4, "Friday", "Cuma", "The office closes early on Friday."),
        (1045, 4, "Saturday", "Cumartesi", "On Saturday we visit my parents."),
        (1046, 4, "Sunday", "Pazar", "Sunday is for resting."),
        (1047, 4, "Week", "Hafta", "I will be away for a week."),
        (1048, 4, "Month", "Ay", "The rent is due every month."),

        // --- Ders 5: Family ---
        (1049, 5, "Mother", "Anne", "My mother is a teacher."),
        (1050, 5, "Father", "Baba", "His father works at the hospital."),
        (1051, 5, "Sister", "Kız kardeş", "My sister lives abroad."),
        (1052, 5, "Brother", "Erkek kardeş", "Her brother plays football."),
        (1053, 5, "Daughter", "Kız evlat", "Their daughter starts school today."),
        (1054, 5, "Son", "Oğul", "Our son is learning to drive."),
        (1055, 5, "Grandmother", "Büyükanne", "My grandmother makes wonderful soup."),
        (1056, 5, "Grandfather", "Büyükbaba", "Grandfather tells the best stories."),
        (1057, 5, "Spouse", "Eş", "My spouse is waiting outside."),
        (1058, 5, "Child", "Çocuk", "The child is asleep."),
        (1059, 5, "Friend", "Arkadaş", "A friend is coming for dinner."),
        (1060, 5, "Family", "Aile", "The whole family came together."),

        // --- Ders 6: Colours ---
        (1061, 6, "Red", "Kırmızı", "She wore a red coat."),
        (1062, 6, "Blue", "Mavi", "The sky is clear and blue."),
        (1063, 6, "Green", "Yeşil", "The garden is very green."),
        (1064, 6, "Yellow", "Sarı", "He painted the door yellow."),
        (1065, 6, "Black", "Siyah", "I bought black shoes."),
        (1066, 6, "White", "Beyaz", "The walls are white."),
        (1067, 6, "Orange", "Turuncu", "The sunset turned orange."),
        (1068, 6, "Purple", "Mor", "These flowers are purple."),
        (1069, 6, "Brown", "Kahverengi", "He has brown eyes."),
        (1070, 6, "Grey", "Gri", "The sky looks grey today."),

        // --- Ders 7: Travel and Directions ---
        (1071, 7, "Airport", "Havalimanı", "The airport is far from the centre."),
        (1072, 7, "Station", "İstasyon", "We met at the station."),
        (1073, 7, "Ticket", "Bilet", "I bought a return ticket."),
        (1074, 7, "Hotel", "Otel", "The hotel is next to the museum."),
        (1075, 7, "Left", "Sol", "Turn left at the corner."),
        (1076, 7, "Right", "Sağ", "The bank is on your right."),
        (1077, 7, "Straight", "Düz", "Go straight for two streets."),
        (1078, 7, "Map", "Harita", "Let me look at the map."),
        (1079, 7, "Luggage", "Bavul", "My luggage is still in the car."),
        (1080, 7, "Passport", "Pasaport", "Please show me your passport."),
        (1081, 7, "Road", "Yol", "This road leads to the sea."),
        (1082, 7, "Bridge", "Köprü", "We crossed the old bridge."),

        // --- Ders 8: Work and School ---
        (1083, 8, "Work", "İş", "I start work at nine."),
        (1084, 8, "Office", "Ofis", "Her office is on the third floor."),
        (1085, 8, "Meeting", "Toplantı", "The meeting lasted an hour."),
        (1086, 8, "Teacher", "Öğretmen", "Our teacher explained it twice."),
        (1087, 8, "Student", "Öğrenci", "Every student needs a notebook."),
        (1088, 8, "School", "Okul", "The school is closed today."),
        (1089, 8, "Book", "Kitap", "This book is easy to read."),
        (1090, 8, "Pen", "Kalem", "Can I borrow your pen?"),
        (1091, 8, "Question", "Soru", "That is a good question."),
        (1092, 8, "Answer", "Cevap", "I do not know the answer."),
        (1093, 8, "Exam", "Sınav", "The exam starts at ten."),
        (1094, 8, "Homework", "Ödev", "He finished his homework early."),

        // --- Ders 9: Body and Health ---
        (1095, 9, "Head", "Baş", "My head hurts a little."),
        (1096, 9, "Hand", "El", "Wash your hands before dinner."),
        (1097, 9, "Eye", "Göz", "Something is in my eye."),
        (1098, 9, "Ear", "Kulak", "He whispered in my ear."),
        (1099, 9, "Heart", "Kalp", "The doctor listened to my heart."),
        (1100, 9, "Doctor", "Doktor", "You should see a doctor."),
        (1101, 9, "Hospital", "Hastane", "The hospital is open all night."),
        (1102, 9, "Medicine", "İlaç", "Take this medicine after meals."),
        (1103, 9, "Pain", "Ağrı", "The pain is much better now."),
        (1104, 9, "Fever", "Ateş", "The child has a fever."),
        (1105, 9, "Foot", "Ayak", "My left foot is swollen."),
        (1106, 9, "Tooth", "Diş", "This tooth has been hurting."),

        // --- Ders 10: Home and Daily Life ---
        (1107, 10, "House", "Ev", "Their house has a red roof."),
        (1108, 10, "Door", "Kapı", "Please close the door."),
        (1109, 10, "Window", "Pencere", "Open the window for some air."),
        (1110, 10, "Kitchen", "Mutfak", "She is cooking in the kitchen."),
        (1111, 10, "Table", "Masa", "Put the plates on the table."),
        (1112, 10, "Chair", "Sandalye", "This chair is broken."),
        (1113, 10, "Bed", "Yatak", "The bed is by the window."),
        (1114, 10, "Key", "Anahtar", "I cannot find my key."),
        (1115, 10, "Room", "Oda", "The room is very quiet."),
        (1116, 10, "Garden", "Bahçe", "We had tea in the garden."),
        (1117, 10, "Car", "Araba", "The car will not start."),
        (1118, 10, "Money", "Para", "I forgot my money at home."),
    };

    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lesson>().HasData(
            Lessons.Select(lesson => new Lesson
            {
                LessonID = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Level = lesson.Level,
                OrderIndex = lesson.Order,
                CreatedAt = SeedCreatedAt
            }));

        modelBuilder.Entity<Vocabulary>().HasData(
            Words.Select(word => new Vocabulary
            {
                WordID = word.WordId,
                // Deliberately null: a curriculum word belongs to the shared
                // lesson plan, not to any learner's deck.
                DeckId = null,
                Term = word.Term,
                Translation = word.Translation,
                ExampleSentence = word.Example,
                CreatedAt = SeedCreatedAt
            }));

        modelBuilder.Entity<LessonVocabulary>().HasData(
            Words.Select(word => new LessonVocabulary
            {
                LessonID = word.LessonId,
                WordID = word.WordId
            }));
    }
}
