using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageTagAndDailySummaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyStudySummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    QuizCount = table.Column<int>(type: "int", nullable: false),
                    LessonCount = table.Column<int>(type: "int", nullable: false),
                    StudySeconds = table.Column<int>(type: "int", nullable: false),
                    XpEarned = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStudySummaries", x => x.Id);
                    table.CheckConstraint("CK_DailyStudySummary_Counters", "[ReviewCount] >= 0 AND [CorrectCount] >= 0 AND [QuizCount] >= 0 AND [LessonCount] >= 0 AND [StudySeconds] >= 0 AND [XpEarned] >= 0 AND [CorrectCount] <= [ReviewCount]");
                    table.ForeignKey(
                        name: "FK_DailyStudySummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FlagCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Code);
                    table.CheckConstraint("CK_Language_SortOrder", "[SortOrder] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.CheckConstraint("CK_Tag_Kind", "[Kind] IN ('Grammar', 'Register', 'Difficulty')");
                });

            migrationBuilder.CreateTable(
                name: "VocabularyTags",
                columns: table => new
                {
                    WordID = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabularyTags", x => new { x.WordID, x.TagId });
                    table.ForeignKey(
                        name: "FK_VocabularyTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocabularyTags_Vocabularies_WordID",
                        column: x => x.WordID,
                        principalTable: "Vocabularies",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "FlagCode", "IsActive", "Name", "NativeName", "SortOrder" },
                values: new object[,]
                {
                    { "de", "de", true, "German", "Deutsch", 5 },
                    { "en", "gb", true, "English", "English", 1 },
                    { "es", "es", true, "Spanish", "Español", 3 },
                    { "fr", "fr", true, "French", "Français", 4 },
                    { "it", "it", true, "Italian", "Italiano", 6 },
                    { "ja", "jp", true, "Japanese", "日本語", 8 },
                    { "ko", "kr", true, "Korean", "한국어", 9 },
                    { "pt", "pt", true, "Portuguese", "Português", 7 },
                    { "tr", "tr", true, "Turkish", "Türkçe", 2 },
                    { "zh", "cn", true, "Chinese", "中文", 10 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "Kind", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "Çekimi kurala uymayan fiiller", "Grammar", "Irregular verb", "irregular-verb" },
                    { 2, "Edatla anlam değiştiren fiiller", "Grammar", "Phrasal verb", "phrasal-verb" },
                    { 3, "İsim", "Grammar", "Noun", "noun" },
                    { 4, "Sıfat", "Grammar", "Adjective", "adjective" },
                    { 5, "Zarf", "Grammar", "Adverb", "adverb" },
                    { 6, "Edat", "Grammar", "Preposition", "preposition" },
                    { 7, "Resmî dilde kullanılır", "Register", "Formal", "formal" },
                    { 8, "Günlük konuşma dili", "Register", "Informal", "informal" },
                    { 9, "Argo", "Register", "Slang", "slang" },
                    { 10, "Akademik metinlerde geçer", "Register", "Academic", "academic" },
                    { 11, "Ana dile benzeyip anlamı farklı olan kelimeler", "Difficulty", "False friend", "false-friend" },
                    { 12, "Öğrenenlerin sık karıştırdığı kelimeler", "Difficulty", "Common mistake", "common-mistake" },
                    { 13, "Günlük dilde en sık geçen kelimeler", "Difficulty", "High frequency", "high-frequency" },
                    { 14, "İleri seviye kelime dağarcığı", "Difficulty", "Advanced", "advanced" }
                });

            migrationBuilder.InsertData(
                table: "VocabularyTags",
                columns: new[] { "TagId", "WordID" },
                values: new object[,]
                {
                    { 13, 1001 },
                    { 13, 1002 },
                    { 13, 1003 },
                    { 13, 1004 },
                    { 13, 1005 },
                    { 13, 1006 },
                    { 13, 1007 },
                    { 13, 1008 },
                    { 13, 1009 },
                    { 13, 1010 },
                    { 13, 1011 },
                    { 13, 1012 },
                    { 3, 1013 },
                    { 13, 1013 },
                    { 3, 1014 },
                    { 13, 1014 },
                    { 3, 1015 },
                    { 13, 1015 },
                    { 3, 1016 },
                    { 13, 1016 },
                    { 3, 1017 },
                    { 13, 1017 },
                    { 3, 1018 },
                    { 13, 1018 },
                    { 3, 1019 },
                    { 13, 1019 },
                    { 3, 1020 },
                    { 13, 1020 },
                    { 3, 1021 },
                    { 13, 1021 },
                    { 3, 1022 },
                    { 13, 1022 },
                    { 3, 1023 },
                    { 13, 1023 },
                    { 3, 1024 },
                    { 13, 1024 },
                    { 13, 1025 },
                    { 13, 1026 },
                    { 13, 1027 },
                    { 13, 1028 },
                    { 13, 1029 },
                    { 13, 1030 },
                    { 13, 1031 },
                    { 13, 1032 },
                    { 13, 1033 },
                    { 13, 1034 },
                    { 13, 1035 },
                    { 13, 1036 },
                    { 5, 1037 },
                    { 13, 1037 },
                    { 5, 1038 },
                    { 13, 1038 },
                    { 5, 1039 },
                    { 13, 1039 },
                    { 3, 1040 },
                    { 13, 1040 },
                    { 3, 1041 },
                    { 13, 1041 },
                    { 3, 1042 },
                    { 13, 1042 },
                    { 3, 1043 },
                    { 13, 1043 },
                    { 3, 1044 },
                    { 13, 1044 },
                    { 3, 1045 },
                    { 13, 1045 },
                    { 3, 1046 },
                    { 13, 1046 },
                    { 3, 1047 },
                    { 13, 1047 },
                    { 3, 1048 },
                    { 13, 1048 },
                    { 3, 1049 },
                    { 13, 1049 },
                    { 3, 1050 },
                    { 13, 1050 },
                    { 3, 1051 },
                    { 13, 1051 },
                    { 3, 1052 },
                    { 13, 1052 },
                    { 3, 1053 },
                    { 13, 1053 },
                    { 3, 1054 },
                    { 13, 1054 },
                    { 3, 1055 },
                    { 13, 1055 },
                    { 3, 1056 },
                    { 13, 1056 },
                    { 3, 1057 },
                    { 13, 1057 },
                    { 3, 1058 },
                    { 13, 1058 },
                    { 3, 1059 },
                    { 13, 1059 },
                    { 3, 1060 },
                    { 13, 1060 },
                    { 4, 1061 },
                    { 13, 1061 },
                    { 4, 1062 },
                    { 13, 1062 },
                    { 4, 1063 },
                    { 13, 1063 },
                    { 4, 1064 },
                    { 13, 1064 },
                    { 4, 1065 },
                    { 13, 1065 },
                    { 4, 1066 },
                    { 13, 1066 },
                    { 4, 1067 },
                    { 13, 1067 },
                    { 4, 1068 },
                    { 13, 1068 },
                    { 4, 1069 },
                    { 13, 1069 },
                    { 4, 1070 },
                    { 13, 1070 },
                    { 3, 1071 },
                    { 3, 1072 },
                    { 3, 1073 },
                    { 3, 1074 },
                    { 5, 1075 },
                    { 5, 1076 },
                    { 5, 1077 },
                    { 3, 1078 },
                    { 3, 1079 },
                    { 3, 1080 },
                    { 3, 1081 },
                    { 3, 1082 },
                    { 3, 1083 },
                    { 10, 1083 },
                    { 3, 1084 },
                    { 10, 1084 },
                    { 3, 1085 },
                    { 10, 1085 },
                    { 3, 1086 },
                    { 10, 1086 },
                    { 3, 1087 },
                    { 10, 1087 },
                    { 3, 1088 },
                    { 10, 1088 },
                    { 3, 1089 },
                    { 10, 1089 },
                    { 3, 1090 },
                    { 10, 1090 },
                    { 3, 1091 },
                    { 10, 1091 },
                    { 3, 1092 },
                    { 10, 1092 },
                    { 3, 1093 },
                    { 10, 1093 },
                    { 3, 1094 },
                    { 10, 1094 },
                    { 3, 1095 },
                    { 3, 1096 },
                    { 3, 1097 },
                    { 3, 1098 },
                    { 3, 1099 },
                    { 3, 1100 },
                    { 3, 1101 },
                    { 3, 1102 },
                    { 3, 1103 },
                    { 3, 1104 },
                    { 3, 1105 },
                    { 3, 1106 },
                    { 3, 1107 },
                    { 3, 1108 },
                    { 3, 1109 },
                    { 3, 1110 },
                    { 3, 1111 },
                    { 3, 1112 },
                    { 3, 1113 },
                    { 3, 1114 },
                    { 3, 1115 },
                    { 3, 1116 },
                    { 3, 1117 },
                    { 3, 1118 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyStudySummaries_UserId_Day",
                table: "DailyStudySummaries",
                columns: new[] { "UserId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_IsActive_SortOrder",
                table: "Languages",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Slug",
                table: "Tags",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyTags_TagId",
                table: "VocabularyTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStudySummaries");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "VocabularyTags");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
