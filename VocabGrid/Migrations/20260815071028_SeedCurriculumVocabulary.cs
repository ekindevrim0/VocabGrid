using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class SeedCurriculumVocabulary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "LessonID", "CreatedAt", "Description", "Level", "OrderIndex", "Title" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Counting from one to a thousand", "A1", 3, "Numbers" },
                    { 4, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Days of the week and talking about when", "A1", 4, "Time and Days" },
                    { 5, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "People closest to you", "A1", 5, "Family" },
                    { 6, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "The colours you need every day", "A1", 6, "Colours" },
                    { 7, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Getting around and finding your way", "A2", 7, "Travel and Directions" },
                    { 8, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Words for the office and the classroom", "A2", 8, "Work and School" },
                    { 9, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Describing how you feel and seeing a doctor", "A2", 9, "Body and Health" },
                    { 10, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Around the house and the everyday", "A2", 10, "Home and Daily Life" }
                });

            migrationBuilder.InsertData(
                table: "Vocabularies",
                columns: new[] { "WordID", "AudioUrl", "CreatedAt", "DeckId", "ExampleSentence", "ImageUrl", "Term", "Translation", "UpdatedAt" },
                values: new object[,]
                {
                    { 1001, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hello, nice to meet you.", null, "Hello", "Merhaba", null },
                    { 1002, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Good morning, did you sleep well?", null, "Good morning", "Günaydın", null },
                    { 1003, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Good evening, please come in.", null, "Good evening", "İyi akşamlar", null },
                    { 1004, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Good night, see you tomorrow.", null, "Good night", "İyi geceler", null },
                    { 1005, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Goodbye, take care of yourself.", null, "Goodbye", "Hoşça kal", null },
                    { 1006, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Thank you for all your help.", null, "Thank you", "Teşekkürler", null },
                    { 1007, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Please wait here for a moment.", null, "Please", "Lütfen", null },
                    { 1008, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sorry, I am running late.", null, "Sorry", "Özür dilerim", null },
                    { 1009, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Excuse me, where is the station?", null, "Excuse me", "Affedersiniz", null },
                    { 1010, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Yes, that sounds good to me.", null, "Yes", "Evet", null },
                    { 1011, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "No, thank you very much.", null, "No", "Hayır", null },
                    { 1012, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Welcome to our home.", null, "Welcome", "Hoş geldiniz", null },
                    { 1013, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Could I have a glass of water?", null, "Water", "Su", null },
                    { 1014, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The bread is still warm.", null, "Bread", "Ekmek", null },
                    { 1015, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She drinks milk every morning.", null, "Milk", "Süt", null },
                    { 1016, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Would you like a cup of tea?", null, "Tea", "Çay", null },
                    { 1017, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I drink coffee after lunch.", null, "Coffee", "Kahve", null },
                    { 1018, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This apple is very sweet.", null, "Apple", "Elma", null },
                    { 1019, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We bought cheese and olives.", null, "Cheese", "Peynir", null },
                    { 1020, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He ate one egg for breakfast.", null, "Egg", "Yumurta", null },
                    { 1021, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The rice is cooked with butter.", null, "Rice", "Pirinç", null },
                    { 1022, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The meat is on the grill.", null, "Meat", "Et", null },
                    { 1023, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Please pass me the salt.", null, "Salt", "Tuz", null },
                    { 1024, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "No sugar in my coffee, thanks.", null, "Sugar", "Şeker", null },
                    { 1025, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I only need one ticket.", null, "One", "Bir", null },
                    { 1026, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We waited two hours.", null, "Two", "İki", null },
                    { 1027, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "There are three rooms upstairs.", null, "Three", "Üç", null },
                    { 1028, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The table seats four people.", null, "Four", "Dört", null },
                    { 1029, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The shop opens at five.", null, "Five", "Beş", null },
                    { 1030, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She has six brothers and sisters.", null, "Six", "Altı", null },
                    { 1031, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The week has seven days.", null, "Seven", "Yedi", null },
                    { 1032, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He works eight hours a day.", null, "Eight", "Sekiz", null },
                    { 1033, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The train leaves at nine.", null, "Nine", "Dokuz", null },
                    { 1034, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Give me ten minutes.", null, "Ten", "On", null },
                    { 1035, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The book has a hundred pages.", null, "Hundred", "Yüz", null },
                    { 1036, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A thousand people came.", null, "Thousand", "Bin", null },
                    { 1037, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Today is a busy day.", null, "Today", "Bugün", null },
                    { 1038, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We will finish it tomorrow.", null, "Tomorrow", "Yarın", null },
                    { 1039, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Yesterday it rained all day.", null, "Yesterday", "Dün", null },
                    { 1040, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The course starts on Monday.", null, "Monday", "Pazartesi", null },
                    { 1041, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I have a meeting on Tuesday.", null, "Tuesday", "Salı", null },
                    { 1042, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wednesday is my day off.", null, "Wednesday", "Çarşamba", null },
                    { 1043, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We travel on Thursday.", null, "Thursday", "Perşembe", null },
                    { 1044, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The office closes early on Friday.", null, "Friday", "Cuma", null },
                    { 1045, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "On Saturday we visit my parents.", null, "Saturday", "Cumartesi", null },
                    { 1046, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sunday is for resting.", null, "Sunday", "Pazar", null },
                    { 1047, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I will be away for a week.", null, "Week", "Hafta", null },
                    { 1048, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The rent is due every month.", null, "Month", "Ay", null },
                    { 1049, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My mother is a teacher.", null, "Mother", "Anne", null },
                    { 1050, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "His father works at the hospital.", null, "Father", "Baba", null },
                    { 1051, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My sister lives abroad.", null, "Sister", "Kız kardeş", null },
                    { 1052, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Her brother plays football.", null, "Brother", "Erkek kardeş", null },
                    { 1053, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Their daughter starts school today.", null, "Daughter", "Kız evlat", null },
                    { 1054, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Our son is learning to drive.", null, "Son", "Oğul", null },
                    { 1055, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My grandmother makes wonderful soup.", null, "Grandmother", "Büyükanne", null },
                    { 1056, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Grandfather tells the best stories.", null, "Grandfather", "Büyükbaba", null },
                    { 1057, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My spouse is waiting outside.", null, "Spouse", "Eş", null },
                    { 1058, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The child is asleep.", null, "Child", "Çocuk", null },
                    { 1059, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A friend is coming for dinner.", null, "Friend", "Arkadaş", null },
                    { 1060, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The whole family came together.", null, "Family", "Aile", null },
                    { 1061, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She wore a red coat.", null, "Red", "Kırmızı", null },
                    { 1062, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sky is clear and blue.", null, "Blue", "Mavi", null },
                    { 1063, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The garden is very green.", null, "Green", "Yeşil", null },
                    { 1064, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He painted the door yellow.", null, "Yellow", "Sarı", null },
                    { 1065, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I bought black shoes.", null, "Black", "Siyah", null },
                    { 1066, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The walls are white.", null, "White", "Beyaz", null },
                    { 1067, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sunset turned orange.", null, "Orange", "Turuncu", null },
                    { 1068, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "These flowers are purple.", null, "Purple", "Mor", null },
                    { 1069, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He has brown eyes.", null, "Brown", "Kahverengi", null },
                    { 1070, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The sky looks grey today.", null, "Grey", "Gri", null },
                    { 1071, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The airport is far from the centre.", null, "Airport", "Havalimanı", null },
                    { 1072, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We met at the station.", null, "Station", "İstasyon", null },
                    { 1073, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I bought a return ticket.", null, "Ticket", "Bilet", null },
                    { 1074, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The hotel is next to the museum.", null, "Hotel", "Otel", null },
                    { 1075, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Turn left at the corner.", null, "Left", "Sol", null },
                    { 1076, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The bank is on your right.", null, "Right", "Sağ", null },
                    { 1077, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Go straight for two streets.", null, "Straight", "Düz", null },
                    { 1078, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Let me look at the map.", null, "Map", "Harita", null },
                    { 1079, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My luggage is still in the car.", null, "Luggage", "Bavul", null },
                    { 1080, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Please show me your passport.", null, "Passport", "Pasaport", null },
                    { 1081, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This road leads to the sea.", null, "Road", "Yol", null },
                    { 1082, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We crossed the old bridge.", null, "Bridge", "Köprü", null },
                    { 1083, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I start work at nine.", null, "Work", "İş", null },
                    { 1084, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Her office is on the third floor.", null, "Office", "Ofis", null },
                    { 1085, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The meeting lasted an hour.", null, "Meeting", "Toplantı", null },
                    { 1086, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Our teacher explained it twice.", null, "Teacher", "Öğretmen", null },
                    { 1087, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Every student needs a notebook.", null, "Student", "Öğrenci", null },
                    { 1088, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The school is closed today.", null, "School", "Okul", null },
                    { 1089, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This book is easy to read.", null, "Book", "Kitap", null },
                    { 1090, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Can I borrow your pen?", null, "Pen", "Kalem", null },
                    { 1091, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "That is a good question.", null, "Question", "Soru", null },
                    { 1092, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I do not know the answer.", null, "Answer", "Cevap", null },
                    { 1093, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The exam starts at ten.", null, "Exam", "Sınav", null },
                    { 1094, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He finished his homework early.", null, "Homework", "Ödev", null },
                    { 1095, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My head hurts a little.", null, "Head", "Baş", null },
                    { 1096, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wash your hands before dinner.", null, "Hand", "El", null },
                    { 1097, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Something is in my eye.", null, "Eye", "Göz", null },
                    { 1098, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "He whispered in my ear.", null, "Ear", "Kulak", null },
                    { 1099, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The doctor listened to my heart.", null, "Heart", "Kalp", null },
                    { 1100, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "You should see a doctor.", null, "Doctor", "Doktor", null },
                    { 1101, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The hospital is open all night.", null, "Hospital", "Hastane", null },
                    { 1102, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Take this medicine after meals.", null, "Medicine", "İlaç", null },
                    { 1103, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The pain is much better now.", null, "Pain", "Ağrı", null },
                    { 1104, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The child has a fever.", null, "Fever", "Ateş", null },
                    { 1105, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "My left foot is swollen.", null, "Foot", "Ayak", null },
                    { 1106, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This tooth has been hurting.", null, "Tooth", "Diş", null },
                    { 1107, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Their house has a red roof.", null, "House", "Ev", null },
                    { 1108, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Please close the door.", null, "Door", "Kapı", null },
                    { 1109, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Open the window for some air.", null, "Window", "Pencere", null },
                    { 1110, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "She is cooking in the kitchen.", null, "Kitchen", "Mutfak", null },
                    { 1111, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Put the plates on the table.", null, "Table", "Masa", null },
                    { 1112, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "This chair is broken.", null, "Chair", "Sandalye", null },
                    { 1113, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The bed is by the window.", null, "Bed", "Yatak", null },
                    { 1114, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I cannot find my key.", null, "Key", "Anahtar", null },
                    { 1115, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The room is very quiet.", null, "Room", "Oda", null },
                    { 1116, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "We had tea in the garden.", null, "Garden", "Bahçe", null },
                    { 1117, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The car will not start.", null, "Car", "Araba", null },
                    { 1118, null, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "I forgot my money at home.", null, "Money", "Para", null }
                });

            migrationBuilder.InsertData(
                table: "LessonVocabularies",
                columns: new[] { "LessonID", "WordID" },
                values: new object[,]
                {
                    { 1, 1001 },
                    { 1, 1002 },
                    { 1, 1003 },
                    { 1, 1004 },
                    { 1, 1005 },
                    { 1, 1006 },
                    { 1, 1007 },
                    { 1, 1008 },
                    { 1, 1009 },
                    { 1, 1010 },
                    { 1, 1011 },
                    { 1, 1012 },
                    { 2, 1013 },
                    { 2, 1014 },
                    { 2, 1015 },
                    { 2, 1016 },
                    { 2, 1017 },
                    { 2, 1018 },
                    { 2, 1019 },
                    { 2, 1020 },
                    { 2, 1021 },
                    { 2, 1022 },
                    { 2, 1023 },
                    { 2, 1024 },
                    { 3, 1025 },
                    { 3, 1026 },
                    { 3, 1027 },
                    { 3, 1028 },
                    { 3, 1029 },
                    { 3, 1030 },
                    { 3, 1031 },
                    { 3, 1032 },
                    { 3, 1033 },
                    { 3, 1034 },
                    { 3, 1035 },
                    { 3, 1036 },
                    { 4, 1037 },
                    { 4, 1038 },
                    { 4, 1039 },
                    { 4, 1040 },
                    { 4, 1041 },
                    { 4, 1042 },
                    { 4, 1043 },
                    { 4, 1044 },
                    { 4, 1045 },
                    { 4, 1046 },
                    { 4, 1047 },
                    { 4, 1048 },
                    { 5, 1049 },
                    { 5, 1050 },
                    { 5, 1051 },
                    { 5, 1052 },
                    { 5, 1053 },
                    { 5, 1054 },
                    { 5, 1055 },
                    { 5, 1056 },
                    { 5, 1057 },
                    { 5, 1058 },
                    { 5, 1059 },
                    { 5, 1060 },
                    { 6, 1061 },
                    { 6, 1062 },
                    { 6, 1063 },
                    { 6, 1064 },
                    { 6, 1065 },
                    { 6, 1066 },
                    { 6, 1067 },
                    { 6, 1068 },
                    { 6, 1069 },
                    { 6, 1070 },
                    { 7, 1071 },
                    { 7, 1072 },
                    { 7, 1073 },
                    { 7, 1074 },
                    { 7, 1075 },
                    { 7, 1076 },
                    { 7, 1077 },
                    { 7, 1078 },
                    { 7, 1079 },
                    { 7, 1080 },
                    { 7, 1081 },
                    { 7, 1082 },
                    { 8, 1083 },
                    { 8, 1084 },
                    { 8, 1085 },
                    { 8, 1086 },
                    { 8, 1087 },
                    { 8, 1088 },
                    { 8, 1089 },
                    { 8, 1090 },
                    { 8, 1091 },
                    { 8, 1092 },
                    { 8, 1093 },
                    { 8, 1094 },
                    { 9, 1095 },
                    { 9, 1096 },
                    { 9, 1097 },
                    { 9, 1098 },
                    { 9, 1099 },
                    { 9, 1100 },
                    { 9, 1101 },
                    { 9, 1102 },
                    { 9, 1103 },
                    { 9, 1104 },
                    { 9, 1105 },
                    { 9, 1106 },
                    { 10, 1107 },
                    { 10, 1108 },
                    { 10, 1109 },
                    { 10, 1110 },
                    { 10, 1111 },
                    { 10, 1112 },
                    { 10, 1113 },
                    { 10, 1114 },
                    { 10, 1115 },
                    { 10, 1116 },
                    { 10, 1117 },
                    { 10, 1118 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1001 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1002 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1003 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1004 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1005 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1006 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1007 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1008 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1009 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1010 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1011 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 1, 1012 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1013 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1014 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1015 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1016 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1017 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1018 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1019 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1020 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1021 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1022 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1023 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 2, 1024 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1025 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1026 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1027 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1028 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1029 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1030 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1031 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1032 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1033 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1034 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1035 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 3, 1036 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1037 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1038 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1039 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1040 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1041 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1042 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1043 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1044 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1045 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1046 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1047 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 4, 1048 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1049 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1050 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1051 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1052 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1053 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1054 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1055 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1056 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1057 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1058 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1059 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 5, 1060 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1061 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1062 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1063 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1064 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1065 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1066 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1067 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1068 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1069 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 6, 1070 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1071 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1072 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1073 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1074 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1075 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1076 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1077 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1078 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1079 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1080 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1081 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 7, 1082 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1083 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1084 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1085 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1086 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1087 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1088 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1089 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1090 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1091 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1092 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1093 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 8, 1094 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1095 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1096 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1097 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1098 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1099 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1100 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1101 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1102 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1103 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1104 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1105 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 9, 1106 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1107 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1108 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1109 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1110 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1111 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1112 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1113 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1114 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1115 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1116 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1117 });

            migrationBuilder.DeleteData(
                table: "LessonVocabularies",
                keyColumns: new[] { "LessonID", "WordID" },
                keyValues: new object[] { 10, 1118 });

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1084);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1085);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1086);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1087);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1088);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1089);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1094);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1095);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1096);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1097);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1098);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1114);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1115);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1116);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1117);

            migrationBuilder.DeleteData(
                table: "Vocabularies",
                keyColumn: "WordID",
                keyValue: 1118);
        }
    }
}
