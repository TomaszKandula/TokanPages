import * as React from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { ActionCreators } from "../../Redux/Actions/selectArticleAction";
import { ARTICLE_PATH, IMAGE_URL } from "../../Shared/constants";
import ArticleCardView from "./articleCardView";

export interface IArticleCard
{
    title: string;
    description: string;
    id: string;
}

export default function ArticleCard(props: IArticleCard)
{
    const content = { button: "Read" };
    const articleUrl = ARTICLE_PATH.replace("{ID}", props.id);
    const imageUrl = IMAGE_URL.replace("{ID}", props.id);

    const dispatch = useDispatch();
    const history = useHistory();

    const onClickEvent = () => 
    {
        dispatch(ActionCreators.selectArticle(props.id));
        history.push(articleUrl);
    };

    return (<ArticleCardView bind=
    {{
        imageUrl: imageUrl,
        title: props.title,
        description: props.description,
        onClickEvent: onClickEvent,
        buttonText: content.button
    }}/>);
}
