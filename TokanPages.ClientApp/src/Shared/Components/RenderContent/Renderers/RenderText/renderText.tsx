import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ArrowRight } from "@material-ui/icons";
import { GET_NON_VIDEO_ASSET } from "../../../../../Api/Request/Paths";
import { ArticleInfoAction } from "../../../../../Store/Actions";
import { ApplicationState } from "../../../../../Store/Configuration";
import { ArticleItemBase } from "../../Models";
import { TextItem } from "../../Models/TextModel";
import { useHash } from "../../../../../Shared/Hooks";
import { ArticleCard, ArticleCardView, ProgressBar, RenderList } from "../../../../../Shared/Components";
import { TComponent } from "../../../../../Shared/types";
import Validate from "validate.js";

interface DataProps {
    value?: string;
    text?: string;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const RenderAnchorLink = (props: DataProps): React.ReactElement => {
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
        <span className="render-text-wrapper" onClick={onClickHandler}>
            <ArrowRight />
            <span className="render-text-common render-text-paragraph render-text-link">
                {props.text ?? NO_CONTENT}
            </span>
        </span>
    );
};

const RenderTargetLink = (props: DataProps): React.ReactElement => {
    return <div id={props.value}>{props.text}</div>;
}

const RenderExternalLink = (props: TextItem): React.ReactElement => {
    const hasImage = !Validate.isEmpty(props.propImg);
    const imageUrl = hasImage ? GET_NON_VIDEO_ASSET.replace("{name}", props.propImg!) : "";

    const onClickEvent = React.useCallback(() => {
        if (!props.value) {
            return;
        }

        window.open(props.value as string, "_blank");
    }, [props.value]);

    return (
        <ArticleCardView
            imageUrl={imageUrl}
            title={props.propTitle ?? ""}
            description={props.propSubtitle ?? ""}
            onClickEvent={onClickEvent}
            buttonText={props.text}
            flagImage={""}
            canAnimate={false}
            styleSmallCard={true}
        />
    );
};

const RenderInternalLink = (props: TextItem): React.ReactElement => {
    const history = useHistory();
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const hasImage = !Validate.isEmpty(props.propImg);
    const imageUrl = hasImage ? GET_NON_VIDEO_ASSET.replace("{name}", props.propImg!) : "";

    const onClickEvent = React.useCallback(() => {
        history.push(`/${languageId}${props.value}`);
    }, [props.value, languageId]);

    return (
        <ArticleCardView
            imageUrl={imageUrl}
            title={props.propTitle ?? ""}
            description={props.propSubtitle ?? ""}
            onClickEvent={onClickEvent}
            buttonText={props.text}
            flagImage={""}
            canAnimate={false}
        />
    );
};

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
    }, [props.value, selection.isLoading, hasCollection, hasInfo, collectedInfo]);

    return selection.isLoading ? (
        <ProgressBar />
    ) : (
        <ArticleCard
            id={info?.id ?? ""}
            title={info?.title ?? ""}
            description={info?.description ?? ""}
            languageIso={info?.languageIso ?? ""}
            canAnimate={false}
            readCount={info?.readCount}
            totalLikes={info?.totalLikes}
        />
    );
};

const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-title mt-56 mb-8">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-sub-title mt-8 mb-40">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

const RenderHeader = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-header mt-56 mb-15">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

const RenderParagraph = (props: TextItem): React.ReactElement => {
    const html = props.value as string | string[];
    const prop = props.prop as TComponent;

    const baseStyle = "render-text-common render-text-paragraph";
    const classStyle = props.text === "" ? baseStyle : `${baseStyle} render-text-${props.text}`;

    switch(prop) {
        case "p": return <p className={classStyle}>{html ?? NO_CONTENT}</p>;
        case "span": return <span className={classStyle}>{html ?? NO_CONTENT}</span>;
        case "ul": return <RenderList list={html as string[]} type="ul" className={classStyle} />;
        case "ol": return <RenderList list={html as string[]} type="ol" className={classStyle} />;
        case "div": return <div className={classStyle}>{html ?? NO_CONTENT}</div>;
        case "br": return <div className="mt-15 mb-15">&nbsp;</div>;
        default: return <p className={classStyle}>{html ?? NO_CONTENT}</p>;
    }
};

const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-paragraph custom-drop-cap">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

export const RenderText = (props: TextItem): React.ReactElement => {
    const value: string = props.value as string;
    switch (props.prop) {
        case "anchor-link":
            return <RenderAnchorLink value={value} text={props.text} />;
        case "target-link":
            return <RenderTargetLink value={value} text={props.text} />;
        case "internal-link":
            return <RenderInternalLink {...props} />;
        case "external-link":
            return <RenderExternalLink {...props} />;
        case "article-link":
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
            return <RenderParagraph {...props} />;
    }
};
