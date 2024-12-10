import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Typography } from "@material-ui/core";
import { ArrowRight } from "@material-ui/icons";
import { ArticleInfoAction } from "../../../../../Store/Actions";
import { ApplicationState } from "../../../../../Store/Configuration";
import { ArticleItemBase } from "../../Models";
import { TextItem } from "../../Models/TextModel";
import { useHash } from "../../../../../Shared/Hooks";
import { ProgressBar } from "../../../../../Shared/Components";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { ArticleCard } from "../../../../../Components/Articles";

interface DataProps {
    value?: string;
    text?: string;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const RenderItemLink = (props: DataProps): React.ReactElement => {
    const hash = useHash();
    const data = props.value;
    const onClickHandler = React.useCallback(() => {
        if (data && data !== "") {
            const element = document?.querySelector(data);
            if (element) {
                element.scrollIntoView({ behavior: "smooth" });
                window.history.pushState(null, "", window.location.pathname + data);
            }
        }
    }, [hash, data]);

    return (
        <Typography
            component="span"
            className="render-text-common render-text-paragraph render-text-link"
            onClick={onClickHandler}
        >
            <span className="render-text-wrapper">
                <ArrowRight />
                <ReactHtmlParser html={props.text ?? NO_CONTENT} />
            </span>
        </Typography>
    );
};

const RenderTextLink = (props: DataProps): React.ReactElement => {
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <Link to={props.value ?? ""}>{props.text}</Link>
        </Typography>
    );
}

const RenderArticleLink = (props: DataProps): React.ReactElement => {
    const dispatch = useDispatch();
    const selection = useSelector((state: ApplicationState) => state.articleInfo);

    const hasCollection = selection.collectedInfo && selection.collectedInfo?.length > 0;
    const collectedInfo = selection.collectedInfo?.filter((value: ArticleItemBase) => value.id === props.value);
    const hasInfo = collectedInfo !== undefined && collectedInfo.length === 1;

    const [info, setInfo] = React.useState<ArticleItemBase | undefined>(undefined);

    React.useEffect(() => {
        if (props.value && props.value !== "" && !hasInfo) {
            dispatch(ArticleInfoAction.get(props.value));
        }
    }, [props.value, hasInfo]);

    React.useEffect(() => {
        if (!selection.isLoading && hasCollection && hasInfo) {
            setInfo(collectedInfo[0]);
        }
    }, [props.value, selection.isLoading,  hasCollection, hasInfo, collectedInfo]);

    return selection.isLoading ? <ProgressBar /> : <ArticleCard 
        id={info?.id ?? ""}
        title={info?.title ?? ""}
        description={info?.description ?? ""}
        languageIso={info?.languageIso ?? ""}
        canAnimate={false}
    />
}

const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 0 }}>
            <Typography component="span" className="render-text-common render-text-title">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 8, marginBottom: 40 }}>
            <Typography component="span" className="render-text-common render-text-sub-title">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderHeader = (props: DataProps): React.ReactElement => {
    return (
        <div style={{ marginTop: 56, marginBottom: 16 }}>
            <Typography component="span" className="render-text-common render-text-header">
                <ReactHtmlParser html={props.value ?? NO_CONTENT} />
            </Typography>
        </div>
    );
};

const RenderParagraph = (props: DataProps): React.ReactElement => {
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={props.value ?? NO_CONTENT} />
        </Typography>
    );
};

const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    const replaced = props.value?.replace("<p>", "<p class='custom-drop-cap'>");
    return (
        <Typography component="span" className="render-text-common render-text-paragraph">
            <ReactHtmlParser html={replaced ?? NO_CONTENT} />
        </Typography>
    );
};

export const RenderText = (props: TextItem): React.ReactElement => {
    const value: string = props.value as string;
    switch (props.prop) {
        case "item-link":
            return <RenderItemLink value={value} text={props.text} />;
        case "text-link":
            return <RenderTextLink value={value} text={props.text} />;    
        case "redirect-link":
            return <RenderArticleLink value={value} text={props.text} />;    
        case "title":
            return <RenderTitle value={value} />;
        case "subtitle":
            return <RenderSubtitle value={value} />;
        case "header":
            return <RenderHeader value={value} />;
        case "dropcap":
            return <RenderParagraphWithDropCap value={value} />;
        default:
            return <RenderParagraph value={value} />;
    }
};
