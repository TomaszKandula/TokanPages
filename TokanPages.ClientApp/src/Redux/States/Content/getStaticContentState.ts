import { ITextObject } from "../../../Shared/Components/RenderContent/Models";

interface IStaticContentItem extends ITextObject
{
    isLoading: boolean;
}

export interface IGetStaticContent
{
    language: string;
    story: IStaticContentItem,
    terms: IStaticContentItem,
    policy: IStaticContentItem
}
