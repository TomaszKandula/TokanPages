import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction } from "../../../Store/Actions";
import { GetShortText } from "../../../Shared/Services/Utilities";
import { MapLanguage } from "../../../Shared/Services/Utilities";
import { ARTICLE_PATH, GET_ARTICLE_MAIN_IMAGE_URL } from "../../../Api";
import { ArticleCardView } from "./View/articleCardView";

interface ArticleCardProps {
    id: string;
    title: string;
    description: string;
    languageIso: string;
    canAnimate: boolean;
    readCount?: number;
    totalLikes?: number;
}

export const ArticleCard = (props: ArticleCardProps): React.ReactElement => {
    const dispatch = useDispatch();
    const history = useHistory();

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);

    const isLoading = data.isLoading;
    const content = data.components.pageArticle;

    const quaryableTitle = props.title.replaceAll(" ", "-").toLowerCase();
    const articleUrl = ARTICLE_PATH.replace("{title}", quaryableTitle);
    const imageUrl = props.id !== "" ? GET_ARTICLE_MAIN_IMAGE_URL.replace("{id}", props.id) : "";

    const onClickEvent = React.useCallback(() => {
        dispatch(ArticleSelectionAction.select({ id: props.id }));
        history.push(`/${languageId}${articleUrl}`);
    }, [props.id, languageId, articleUrl]);

    const flagImage = MapLanguage(props.languageIso);

    const readCount =
        props.readCount !== undefined
            ? props.readCount.toLocaleString(undefined, { minimumFractionDigits: 0 })
            : undefined;

    const totalLikes =
        props.totalLikes !== undefined
            ? props.totalLikes.toLocaleString(undefined, { minimumFractionDigits: 0 })
            : undefined;

    return (
        <ArticleCardView
            isLoading={isLoading}
            imageUrl={imageUrl}
            title={GetShortText({ value: props.title, limit: 6 })}
            description={GetShortText({ value: props.description, limit: 12 })}
            onClickEvent={onClickEvent}
            buttonText={content?.button}
            flagImage={flagImage}
            canAnimate={props.canAnimate}
            readCount={readCount}
            totalLikes={totalLikes}
        />
    );
};
