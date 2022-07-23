import { ITextObject } from "../../../Shared/Components/RenderContent/Models";

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
