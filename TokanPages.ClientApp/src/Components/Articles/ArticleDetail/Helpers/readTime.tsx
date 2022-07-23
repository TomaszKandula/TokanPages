import { ObjectToText } from "../../../../Shared/Services/Converters";
import { CountWords, GetReadTime } from "../../../../Shared/Services/Utilities";
import { ITextItem, ITextObject } from "../../../../Shared/Components/RenderContent/Models";
import { WORDS_PER_MINUTE } from "../../../../Shared/constants";

export const ReadTime = (text: ITextItem[]): string =>
{
    let object: ITextObject = 
    { 
        items: [{ id: "", type: "", value: "", prop: "", text: "" }]
    };
        
    object.items = text;
    const rawText = ObjectToText({ textObject: object });
    const words = CountWords({ inputText: rawText });

    return GetReadTime({ countWords: words, wordsPerMinute: WORDS_PER_MINUTE });
}
