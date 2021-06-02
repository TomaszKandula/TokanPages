import { ITextItem, ITextObject } from "../../../Shared/Components/ContentRender/Models/textModel";
import { CountWords, GetReadTime, TextObjectToRawText } from "../../../Shared/helpers";
import { WORDS_PER_MINUTE } from "../../../Shared/constants";

export const ReadTime = (text: ITextItem[]): string =>
{
    let textObject: ITextObject = 
    { 
        items: [{ id: "", type: "", value: "", prop: "", text: "" }]
    };
        
    textObject.items = text;
    const rawText = TextObjectToRawText(textObject);
    const words = CountWords(rawText);

    return GetReadTime(words, WORDS_PER_MINUTE);
}
