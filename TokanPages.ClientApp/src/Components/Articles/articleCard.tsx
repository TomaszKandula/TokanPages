import * as React from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { ActionCreators } from "../../Redux/Actions/Articles/selectArticleAction";
import { GetShortText } from "../../Shared/helpers";
import { ARTICLE_PATH, IMAGE_URL } from "../../Shared/constants";
import ArticleCardView from "./articleCardView";

export interface IArticleCard
{
    title: string;
    description: string;
    id: string;
}

const ArticleCard = (props: IArticleCard): JSX.Element =>
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
        title: GetShortText(props.title, 6),
        description: GetShortText(props.description, 12),
        onClickEvent: onClickEvent,
        buttonText: content.button
    }}/>);
}

export default ArticleCard;
