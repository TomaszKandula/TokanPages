import * as React from "react";
import { ProgressBar, RenderContent } from "../../../../Shared/Components";
import { ITextItem } from "../../../../Shared/Components/RenderContent/Models";
import Validate from "validate.js";

export const ArticleContent = (guid: string, isLoading: boolean, text: ITextItem[]) =>
{
    if (Validate.isEmpty(guid) || isLoading)
    {
        return(<ProgressBar />);
    }

    return(<RenderContent items={text} />);
};
