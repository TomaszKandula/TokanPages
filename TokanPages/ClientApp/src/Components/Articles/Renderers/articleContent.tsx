import * as React from "react";
import CenteredCircularLoader from "../../../Shared/Components/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../../Shared/Components/ContentRender/renderContent";
import { ITextItem } from "../../../Shared/Components/ContentRender/Models/textModel";
import Validate from "validate.js";

export const ArticleContent = (guid: string, isLoading: boolean, text: ITextItem[]) =>
{
    if (Validate.isEmpty(guid) || isLoading)
    {
        return(<CenteredCircularLoader />);
    }

    return(<RenderContent items={text} />);
};
