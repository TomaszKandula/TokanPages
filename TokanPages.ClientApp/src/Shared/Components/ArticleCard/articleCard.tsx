import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction } from "../../../Store/Actions";
import { GetShortText } from "../../../Shared/Services/Utilities";
import { MapLanguageId } from "../../../Shared/Services/languageService";
import { ARTICLE_PATH, GET_ARTICLE_MAIN_IMAGE_URL } from "../../../Api/Request";
import { ArticleCardView } from "./View/articleCardView";

interface ArticleCardProps {
    id: string;
    title: string;
    description: string;
    languageIso: string;
    canAnimate: boolean;
    readCount?: number;
}

export const ArticleCard = (props: ArticleCardProps): React.ReactElement => {
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.article);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const quaryableTitle = props.title.replaceAll(" ", "-").toLowerCase();
    const articleUrl = ARTICLE_PATH.replace("{title}", quaryableTitle);
    const imageUrl = props.id !== "" ? GET_ARTICLE_MAIN_IMAGE_URL.replace("{id}", props.id) : "";

    const dispatch = useDispatch();
    const history = useHistory();

    const onClickEvent = React.useCallback(() => {
        dispatch(ArticleSelectionAction.select({ id: props.id }));
        history.push(`/${languageId}${articleUrl}`);
    }, [props.id, languageId, articleUrl]);

    const flagImage = MapLanguageId(props.languageIso);

    return (
        <ArticleCardView
            imageUrl={imageUrl}
            title={GetShortText({ value: props.title, limit: 6 })}
            description={GetShortText({ value: props.description, limit: 12 })}
            onClickEvent={onClickEvent}
            buttonText={content?.button}
            flagImage={flagImage}
            canAnimate={props.canAnimate}
            readCount={props.readCount}
        />
    );
};
