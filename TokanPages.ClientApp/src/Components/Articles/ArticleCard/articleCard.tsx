import * as React from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { ArticleSelectionAction } from "../../../Store/Actions";
import { GetShortText } from "../../../Shared/Services/Utilities";
import { ARTICLE_PATH, GET_ARTICLE_MAIN_IMAGE_URL } from "../../../Api/Request";
import { ArticleCardView } from "./View/articleCardView";

interface Properties {
    title: string;
    description: string;
    id: string;
}

export const ArticleCard = (props: Properties): JSX.Element => {
    const content = { button: "Read" };
    const articleUrl = ARTICLE_PATH.replace("{id}", props.id);
    const imageUrl = GET_ARTICLE_MAIN_IMAGE_URL.replace("{id}", props.id);

    const dispatch = useDispatch();
    const history = useHistory();

    const onClickEvent = React.useCallback(() => {
        dispatch(ArticleSelectionAction.select(props.id));
        history.push(articleUrl);
    }, [props.id, articleUrl]);

    return (
        <ArticleCardView
            imageUrl={imageUrl}
            title={GetShortText({ value: props.title, limit: 6 })}
            description={GetShortText({ value: props.description, limit: 12 })}
            onClickEvent={onClickEvent}
            buttonText={content.button}
        />
    );
};
