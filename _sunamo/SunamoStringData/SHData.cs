namespace SunamoStringReplace._sunamo.SunamoStringData;

/// <summary>
/// Zde musí být vše veřejné protože se to používá i v jiných třídách než je ve SH
/// A taky protože někdy od SHData dědit nemůžu ale hodilo by se např. od SHSH
/// </summary>
internal class SHData
{
    internal static bool s_cs = false;
    internal const string postfixSpaceCommaNewline = " (Space, comma, newline delimited)";
    internal static List<string> spaceCommaNewline = new List<string>([AllStrings.space, AllStrings.comma, Environment.NewLine]);
    /// <summary>
    /// Used 
    /// </summary>
    internal const string diacritic = "\u00E1\u010D\u010F\u00E9\u011B\u00ED\u0148\u00F3\u0161\u0165\u00FA\u016F\u00FD\u0159\u017E\u00C1\u010C\u010E\u00C9\u011A\u00CD\u0147\u00D3\u0160\u0164\u00DA\u016E\u00DD\u0158\u017D";
    internal static List<string> charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma = null;
    internal static List<string> charsForSplitPunctuationCharsAndWhiteSpacesWithComma = null;
    //private static bool s_initDiactitic = false;
    static bool _result = false;
    internal static bool Result
    {
        get
        {
            return _result;
        }
        set
        {
            _result = value;
        }
    }
    /// <summary>
    /// Dont contains
    /// </summary>
    internal static char[] spaceAndPuntactionChars = new char[] { AllChars.space, AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.colon, AllChars.excl, AllChars.q, '\u2013', '\u2014', '\u2010', '\u2026', '\u201E', '\u201C', '\u201A', '\u2018', '\u00BB', '\u00AB', '\u2019', AllChars.bs, AllChars.lb, AllChars.rb, AllChars.rsqb, AllChars.lsqb, AllChars.lcub, AllChars.rcub, '\u3008', '\u3009', AllChars.lt, AllChars.gt, AllChars.slash, AllChars.bs, AllChars.verbar, '\u201D', AllChars.qm, '~', '\u00B0', AllChars.plus, '@', '#', '$', AllChars.percnt, '^', '&', AllChars.asterisk, '=', AllChars.lowbar, '\u02C7', '\u00A8', '\u00A4', '\u00F7', '\u00D7', '\u02DD' };
    internal static char[] s_spaceAndPuntactionCharsAndWhiteSpaces = null;
    internal static void Init()
    {
        //charsForSplitPunctuationCharsAndWhiteSpacesWithComma = ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(true);
        //charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma = ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(false);
        List<char> spaceAndPuntactionCharsAndWhiteSpacesList = new List<char>();
        spaceAndPuntactionCharsAndWhiteSpacesList.AddRange(spaceAndPuntactionChars);
        foreach (var item in AllChars.whiteSpacesChars)
        {
            if (!spaceAndPuntactionCharsAndWhiteSpacesList.Contains(item))
            {
                spaceAndPuntactionCharsAndWhiteSpacesList.Add(item);
            }
        }
        List<char> a = null;
        //,' | ',
        // removed char 18 - it was recognized as control char
        a = new List<char>(['�', 'ل', '€', '™', ':', '´', '', '\'', 'ṗ', '`', '§', '←', '↑', '¡', '↓', '³', '©', '¿', 'ƒ', '¸', '¹', 'а', 'ﬂ', 'і', 'Н', 'е', 'б', 'с', 'х', 'л', 'Е', 'ў', 'р', 'о', 'п', 'ы', '®', 'С', 'ч', 'т', 'ь', 'н', 'д', 'ж', 'к', 'я', 'О', 'в', 'ю', 'Э', 'М', 'м', 'и', 'з', 'Б', 'ц', 'ш', 'В', 'Т', 'г', 'э', 'у', 'Д', 'Я', 'П', 'А', 'щ', 'Ю', 'И', 'Г', 'У', 'К', 'Ч', 'Р', 'З', 'Л', 'Х', 'ф', 'Ж', 'Ц', '', 'Ṩ', '¬', '既', '然', '愛', '讓', '你', '找', '到', '對', '的', '我', '下', '去', '如', '此', '真', '切', '其', '他', '都', '無', '所', '求', '早', '就', '懼', '任', '何', '危', '險', '該', '一', '起', '往', '前', '走', '卻', '放', '手', '果', '能', '時', '間', '折', '返', '回', '那', '天', '們', '分', '開', '會', '改', '變', '個', '答', '案', '˜', 'ả', 'ấ', 'प', 'ा', 'ठ', 'न', 'ह', 'ी', 'ं', 'म', 'ि', 'ल', '', '‒', '♥', 'み', 'ん', 'な', '最', '高', 'あ', 'り', 'が', 'と', 'う', 'か', 'わ', 'い', 'Λ', '歪', 'だ', '身', '体', '叫', 'び', '出', 'す', '痛', 'つ', 'け', 'る', '汚', '世', '界', 'っ', 'た', '翼', '飛', 'べ', 'ら', '支', '配', '恐', 'れ', '偽', '善', '者', 'て', '捨', 'ち', 'ま', 'え', 'よ', '犠', '牲', '傷', '言', '葉', '届', 'く', '願', '叶', '光', '奪', '絆', '終', '誓', '忘', 'こ', 'の', '壊', 'も', '☆', '★', '¯', '­', '″', 'Ṗ', '±', '⁕', 'ا', 'ه', 'γ', 'ζ', 'μ', 'α', 'ε', 'υ', 'ω', '£', '♫', '。', '♯', '⚄', '∞', 'ʇ', '∑', 'ʼ', 'ª', '¦', 'ˆ', '¥', 'ɛ', 'ɑ', '̃', 'ŋ', '', '·', '∈', '', '', '‽', '♦', '', '', '', 'ب', 'ن', 'ی', 'د', 'م', 'ع', 'ک', 'گ', 'ر', 'ف', 'ز', 'و', 'چ', '،', 'ق', 'ت', 'ح', '‌', '′', '', 'Þ', 'ﬁ', 'º', '•', 'س', 'ج', 'ژ', 'ص', 'ټ', 'ړ', 'ښ', 'ĕ', 'Ф', 'ԁ', 'ѕ', 'Ɩ', 'ο', 'ɡ', '', '‡', '‐', '，', '¶', '閽', '抳', '抦', '抰', '抮', '抯', '扵', '抣', '', '', '̌', '́', '', '', '', '²', '♪', '御', '味', '方', '贈', '物', 'は', '同', 'じ', '波', '神', '様', '使', 'ǐ', 'ǎ', 'א', 'נ', 'י', 'ח', 'ו', 'ש', 'ב', 'ע', 'ל', 'ך', 'כ', 'ה', 'מ', 'ד', 'ת', 'צ', 'ק', 'ר', 'ג', 'ם', 'פ', 'ן', 'ז', 'ס', '이', '흐', '름', '을', '타', '새', '로', '운', '길', '함', '께', '천', '히', '올', '라', '진', '실', '닿', '는', '순', '간', '느', '껴', '봐', '눈', '감', '아', '서', '필', '꿈', '과', '현', '내', '손', '잡', '으', '면', 'Ɏ', '‹', '¢', 'ϟ', '˘', '→', 'ə', 'ǰ', 'ḥ', 'ʾ', 'ṭ', 'ʿ', 'ṣ', 'ḍ', 'Ṣ', '‬', 'ŭ', 'Ŭ', 'Ṭ', 'Ɛ', 'ɔ', 'Ɔ', 'ŗ', 'ǧ', 'ġ', 'ḏ', 'þ', 'ṅ', 'ẏ', 'ṃ', 'ṇ', 'ʹ', 'ḹ', 'ң', 'Қ', 'қ', 'ḫ', 'ų', 'ŵ', 'ħ', 'Ħ', 'ṛ', 'ẓ', '̲', '̤', 'Ẕ', 'ŏ', 'ʻ', 'Ḥ', 'İ', 'ệ', 'ứ', 'ố', 'ư', 'ớ', 'ồ', 'ờ', 'ậ', 'ề', 'ế', 'ắ', 'ừ', 'ữ', 'ơ', 'ầ', 'µ', '❌', 'き', 'し', 'む', '‰', '⟓', '娘', '子', '汉', '精', '采', '‎', 'Μ', 'Ι', 'Α', 'Ο', 'Τ', 'Ν', 'Β', 'Η', '、', '†', 'ა', 'ფ', 'ხ', 'ზ', 'უ', 'რ', 'ი', 'ს', 'მ', 'ღ', 'ე', 'ნ', 'ბ', 'ლ', 'ო', 'Ш', 'є', 'Щ', 'І', 'Ь', 'Є', 'Ы', '우', '리', '지', '구', '를', '사', '랑', '해', '요', '们', '爱', '地', '球', 'һ', 'ν', '持', '上', 'げ', '解', '', 'ー', '「', '」', '❤', '〝', '〟', '姫', '僕', '未', '来', 'ァ', '♂', '⇒', 'ッ', '星', '在', '処', '‪', '‏', '笑', '容', '兩', '點', '鐘', '用', '睫', '毛', '剪', '接', '每', '舉', '動', '髮', '飄', '節', '奏', '像', '是', '獨', '幫', '伴', '心', '房', '季', '沒', '有', '春', '秋', '冬', '炎', '熱', '夏', '曬', '臉', '蛋', '紅', '晶', '瑩', '眼', '眸', '情', '書', '般', '剔', '透', '交', '給', '來', '拆', '封', '慢', '著', '作', '定', '格', '輪', '廓', '品', '嚐', '互', '緩', '掌', '控', '完', '美', '瑕', '火', '候', '忍', '住', '呼', '吸', '還', '氣', '相', '投', '即', '不', '邊', '也', '記', '朦', '朧', '秒', '喚', '玫', '瑰', '香', '漫', '游', '頭', '要', '成', '功', '闖', '關', '迷', '宮', '音', '樂', '噗', '通', '板', '胸', '懷', '脈', '調', '保', '證', '跟', '別', '人', '絕', '與', '眾', '吻', '家', '普', '侯', '空', '轉', '陪', '夢', '受', '寵', '為', '訂', '做', '專', '屬', '私', '宇', '宙', '⁠', '►', 'ι', 'λ', 'ς', 'ἴ', '½', 'さ', 'で', 'に', 'ず', 'ど', 'へ', '見', 'を', 'め', '嘘', 'お', '絶', '妙', 'バ', 'ラ', 'ン', 'ス', '首', '皮', '枚', 'ア', 'ダ', 'ル', 'ト', 'ツ', '背', '向', 'ば', 'ほ', '合', '離', 'そ', '形', '確', '探', '明', '日', '君', '怖', 'ぐ', '強', '深', '入', '関', '係', '繋', '糸', '赤', 'せ', 'マ', 'ジ', 'ク', '種', 'ウ', 'ソ', '重', 'ね', '術', '立', '尽', 'ぶ', '声', 'エ', 'グ', '感', '涙', '二', '引', '冷', '独', 'ロ', 'リ', 'ナ', '遠', '寄', '道', '意', '・', '全', '覚', 'カ', 'サ', 'ブ', 'タ', '欠', '落', 'オ', 'メ', 'シ', 'ョ', 'キ', 'ミ', 'イ', '列', '車', '混', '雑', 'コ', 'ュ', 'ニ', 'ケ', '信', '降', '止', '雨', '響', '曲', '現', '代', '失', '度', '目', '気', 'づ', 'ワ', 'デ', '望', 'や', '希', '鏡', '映', '問', '自', '誤', '魔', '化', '生', '酷', '鼓', '景', '色', '掴', '', '♚', 'ℑ', 'ℵ', 'ℜ', '̆', 'Ｉ', 'Ｍ', 'Ａ', 'Ｙ', 'Ｔ', '̈', '̊', '外', '国', '', '', '¼', '歌', '｢', '？', '‿', 'Φ', 'Γ', 'Ε', 'Σ', 'Κ', 'Υ', 'Ξ', 'Χ', 'Π', 'Δ', 'Ρ', 'Θ', 'Ζ', 'Ω', 'Մ', 'ե', 'ն', 'ք', 'մ', 'ի', 'շ', 'տ', 'ա', 'պ', 'ր', 'լ', 'յ', 'ս', 'հ', 'ո', 'ղ', 'ւ', 'Ք', 'ձ', 'դ', 'Ս', 'բ', 'խ', 'գ', 'ց', 'կ', 'ծ', 'ռ', 'Հ', 'վ', 'Ա', 'զ', 'Դ', 'չ', 'ը', 'ժ', 'թ', 'է', 'օ', '։', 'Գ', 'Վ', 'Ի', 'Տ', 'Բ', 'Ե', 'Օ', 'Թ', 'ջ', 'Ռ', '￼', '好', '想', '听', '现', '哪', '里', '当', '睡', '正', '醒', '承', '了', '对', '思', '念', '闭', '看', '脸', '已', '经', '算', '清', '时', '间', '让', '己', '忙', '得', '没', '隙', '否', '则', '脑', '袋', '面', '特', '别', '多', '和', '个', '太', '阳', '升', '얼', '어', '붙', '버', '린', '팔', '찌', '목', '에', '음', '장', '식', '난', '바', '닷', '물', '다', '섭', '취', '마', '치', '범', '고', '래', '된', '것', '같', '나', '셀', '수', '없', '돈', '원', '보', '조', '르', '려', '걸', '니', '방', '울', '의', '온', '몸', '주', '먹', '석', '굴', '모', '양', '별', '귀', '끊', '임', '뿜', '빛', '들', '은', '친', '피', '날', '하', '늘', '위', '너', '무', '가', '까', '워', '여', '긴', '소', '공', '포', '증', '불', '안', '정', '통', '절', '대', '오', '않', '뭄', '명', '품', '전', '부', '달', '쳐', '본', '두', '쥐', '봉', '적', '향', '쏜', '기', '옥', '근', '저', '멀', 'Ј', 'ј', 'ћ', 'љ', 'њ', 'ђ', '', 'ᴏ', 'ʀ', 'ᴅ', '΄', 'ḃ', 'ṫ', 'ṁ', 'ḋ', '䨬', '䳥', 'ߣ', 'ߠ', '䲠', '䨲', 'ߥ', '詞', '', '', 'ي', 'ك', '‫', '', '٧', 'ط', 'ّ', 'ሠ', 'ላ', 'ም', 'ለ', 'ኪ', '؟', 'ܝ', 'ܘ', 'ܣ', 'ܦ', 'ܟ', 'ܢ', 'ܐ', 'χ', 'τ', 'σ', 'π', 'ρ', 'κ', 'η', 'δ', 'ξ', 'φ', 'ψ', 'β', 'θ', 'Ў', '燦', '爛', '聖', '夜', '靜', 'ھ', 'ہ', 'ے', '؛', '慍', '扴', '慠', 'ǒ', 'ǔ', 'ǚ', '蓝', '（', '棒']);
        #region Added as int because its control chars and isBinary could return true => replace in files wouldnt working
        a.Add((char)1);
        a.Add((char)20);
        a.Add((char)159);
        a.Add((char)3);
        a.Add((char)2);
        #endregion
        spaceAndPuntactionCharsAndWhiteSpacesList.AddRange(a);
        spaceAndPuntactionCharsAndWhiteSpacesList = spaceAndPuntactionCharsAndWhiteSpacesList.Distinct().ToList();
        s_spaceAndPuntactionCharsAndWhiteSpaces = spaceAndPuntactionCharsAndWhiteSpacesList.ToArray();
    }
    internal static List<string> ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(bool commaInclude)
    {
        if (charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma == null)
        {
            charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma = spaceAndPuntactionChars.Select(d => d.ToString()).ToList();
        }
        if (commaInclude)
        {
            if (charsForSplitPunctuationCharsAndWhiteSpacesWithComma == null)
            {
                var result = charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma.ToList();
                result.Add(AllStrings.comma);
                charsForSplitPunctuationCharsAndWhiteSpacesWithComma = result;
            }
            return charsForSplitPunctuationCharsAndWhiteSpacesWithComma;
        }
        return charsForSplitPunctuationCharsAndWhiteSpacesWithoutComma;
    }
}