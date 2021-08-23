import { ITextObject } from "../../../Shared/Components/ContentRender/Models/textModel";

interface IStaticContentItem extends ITextObject
{
    isLoading: boolean;
}

export interface IGetStaticContent
{
    story: IStaticContentItem,
    terms: IStaticContentItem,
    policy: IStaticContentItem
}
