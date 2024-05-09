import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction } from "../../../Store/Actions";
import { GetShortText } from "../../../Shared/Services/Utilities";
import { ARTICLE_PATH, GET_ARTICLE_MAIN_IMAGE_URL } from "../../../Api/Request";
import { ArticleCardView } from "./View/articleCardView";

interface Properties {
    id: string;
    title: string;
    description: string;
    languageIso: string;
}

export const ArticleCard = (props: Properties): JSX.Element => {
    const content = useSelector((state: ApplicationState) => state.contentArticle);
    const quaryableTitle = props.title.replaceAll(" ", "-").toLowerCase();
    const articleUrl = ARTICLE_PATH.replace("{title}", quaryableTitle);
    const imageUrl = GET_ARTICLE_MAIN_IMAGE_URL.replace("{id}", props.id);

    const dispatch = useDispatch();
    const history = useHistory();

    const onClickEvent = React.useCallback(() => {
        dispatch(ArticleSelectionAction.select({ id: props.id }));
        history.push(articleUrl);
    }, [props.id, articleUrl]);

    let flagImage = "";
    switch (props.languageIso.toLowerCase()) {
        case "eng":
            flagImage = "eng.png";
            break;
        case "fra":
            flagImage = "fra.png";
            break;
        case "ger":
            flagImage = "ger.png";
            break;
        case "pol":
            flagImage = "pol.png";
            break;
        case "esp":
            flagImage = "esp.png";
            break;
        case "ukr":
            flagImage = "ukr.png";
            break;
        default:
            flagImage = "eng.png";
    }

    return (
        <ArticleCardView
            imageUrl={imageUrl}
            title={GetShortText({ value: props.title, limit: 6 })}
            description={GetShortText({ value: props.description, limit: 12 })}
            onClickEvent={onClickEvent}
            buttonText={content?.content?.button}
            flagImage={flagImage}
        />
    );
};
