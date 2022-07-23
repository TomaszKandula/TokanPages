import * as React from "react";
import { ProgressBar } from "../../../../Shared/Components";
import { RenderContent } from "../../../../Shared/Components";
import { ITextItem } from "../../../../Shared/Components/ContentRender/Models";
import Validate from "validate.js";

export const ArticleContent = (guid: string, isLoading: boolean, text: ITextItem[]) =>
{
    if (Validate.isEmpty(guid) || isLoading)
    {
        return(<ProgressBar />);
    }

    return(<RenderContent items={text} />);
};
