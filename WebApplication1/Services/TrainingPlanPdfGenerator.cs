using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Services;

public static class TrainingPlanPdfGenerator
{
    public static byte[] Generate(PlanTreningowy plan)
    {
        var lines = BuildLines(plan);
        var content = BuildContent(lines);
        var objects = new List<string>
        {
            "<< /Type /Catalog /Pages 2 0 R >>",
            "<< /Type /Pages /Kids [3 0 R] /Count 1 >>",
            "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>",
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>",
            $"<< /Length {Encoding.ASCII.GetByteCount(content)} >>\nstream\n{content}\nendstream"
        };

        return BuildPdf(objects);
    }

    private static List<string> BuildLines(PlanTreningowy plan)
    {
        var lines = new List<string>
        {
            $"Plan treningowy: {plan.Nazwa}",
            $"Poziom: {plan.PoziomZaawansowania}",
            $"Czas trwania: {plan.CzasTrwaniaTygodnie} tyg.",
            $"Opis: {plan.Opis ?? "-"}",
            "",
            "Cwiczenia:"
        };

        if (!plan.PozycjePlanu.Any())
        {
            lines.Add("- Brak cwiczen w planie.");
            return lines;
        }

        foreach (var pozycja in plan.PozycjePlanu.OrderBy(item => item.Cwiczenie.Nazwa))
        {
            lines.Add($"- {pozycja.Cwiczenie.Nazwa}");
            lines.Add($"  Partia: {pozycja.Cwiczenie.PartiaMiesniowa.Nazwa}");
            lines.Add($"  Serie: {pozycja.LiczbaSerii}, powtorzenia: {pozycja.LiczbaPowtorzen}, przerwa: {pozycja.PrzerwaSekundy} s");
        }

        return lines;
    }

    private static string BuildContent(IEnumerable<string> lines)
    {
        var builder = new StringBuilder();
        builder.AppendLine("BT");
        builder.AppendLine("/F1 12 Tf");
        builder.AppendLine("50 800 Td");
        builder.AppendLine("14 TL");

        foreach (var line in lines.SelectMany(WrapLine).Take(52))
        {
            builder.AppendLine($"({Escape(line)}) Tj");
            builder.AppendLine("T*");
        }

        builder.AppendLine("ET");
        return builder.ToString();
    }

    private static IEnumerable<string> WrapLine(string line)
    {
        const int maxLength = 88;

        if (line.Length <= maxLength)
        {
            yield return line;
            yield break;
        }

        for (var index = 0; index < line.Length; index += maxLength)
        {
            yield return line.Substring(index, Math.Min(maxLength, line.Length - index));
        }
    }

    private static string Escape(string text)
    {
        return RemoveUnsupportedCharacters(text)
            .Replace("\\", "\\\\")
            .Replace("(", "\\(")
            .Replace(")", "\\)");
    }

    private static string RemoveUnsupportedCharacters(string text)
    {
        var builder = new StringBuilder(text.Length);

        foreach (var character in text)
        {
            builder.Append(character <= 127 ? character : RemoveDiacritic(character));
        }

        return builder.ToString();
    }

    private static char RemoveDiacritic(char character)
    {
        return character switch
        {
            'ą' or 'Ą' => 'a',
            'ć' or 'Ć' => 'c',
            'ę' or 'Ę' => 'e',
            'ł' or 'Ł' => 'l',
            'ń' or 'Ń' => 'n',
            'ó' or 'Ó' => 'o',
            'ś' or 'Ś' => 's',
            'ż' or 'Ż' or 'ź' or 'Ź' => 'z',
            _ => '?'
        };
    }

    private static byte[] BuildPdf(List<string> objects)
    {
        var builder = new StringBuilder();
        var offsets = new List<int> { 0 };

        builder.AppendLine("%PDF-1.4");

        for (var index = 0; index < objects.Count; index++)
        {
            offsets.Add(Encoding.ASCII.GetByteCount(builder.ToString()));
            builder.AppendLine($"{index + 1} 0 obj");
            builder.AppendLine(objects[index]);
            builder.AppendLine("endobj");
        }

        var xrefOffset = Encoding.ASCII.GetByteCount(builder.ToString());
        builder.AppendLine("xref");
        builder.AppendLine($"0 {objects.Count + 1}");
        builder.AppendLine("0000000000 65535 f ");

        foreach (var offset in offsets.Skip(1))
        {
            builder.AppendLine($"{offset:0000000000} 00000 n ");
        }

        builder.AppendLine("trailer");
        builder.AppendLine($"<< /Size {objects.Count + 1} /Root 1 0 R >>");
        builder.AppendLine("startxref");
        builder.AppendLine(xrefOffset.ToString());
        builder.AppendLine("%%EOF");

        return Encoding.ASCII.GetBytes(builder.ToString());
    }
}
