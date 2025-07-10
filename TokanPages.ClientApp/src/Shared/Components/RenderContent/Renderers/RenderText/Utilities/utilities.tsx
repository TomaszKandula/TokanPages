import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { GET_NON_VIDEO_ASSET } from "../../../../../../Api/Paths";
import { ArticleInfoAction } from "../../../../../../Store/Actions";
import { ApplicationState } from "../../../../../../Store/Configuration";
import { ArticleItemBase } from "../../../Models";
import { TextItem } from "../../../Models/TextModel";
import { useDimensions, useHash } from "../../../../../../Shared/Hooks";
import { ArticleCard, ArticleCardView, Icon, ProgressBar, RenderList } from "../../../../../../Shared/Components";
import { TComponent } from "../../../../../../Shared/types";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./utilities.css";

interface DataProps {
    value?: string;
    text?: string;
}

interface LinkProps {
    href: string;
    target: string;
    rel: string;
    text: string;
}

interface ProcessParagraphsProps {
    html: string | undefined;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const GetFontStyle = (value: string): string => {
    switch (value) {
        /* TEXT WEIGHT */
        case "light": return "has-text-weight-light";
        case "normal": return "has-text-weight-normal";
        case "medium": return "has-text-weight-medium";
        case "semibold": return "has-text-weight-semibold";
        case "bold": return "has-text-weight-bold";
        case "extrabold": return "has-text-weight-extrabold";

        /* TEXT TRANSFORMATION */
        case "capitalized": return "is-capitalized";
        case "lowercase": return "is-lowercase";
        case "uppercase": return "is-uppercase";
        case "italic": return "is-italic";
        case "underlined": return "is-underlined";

        /* FALLBACK */
        default: return "";
    }
}

/* LINK COMPONENTS */

export const RenderAnchorLink = (props: DataProps): React.ReactElement => {
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
        <span className="is-flex py-2 is-align-items-center" onClick={onClickHandler}>
            <Icon name="MenuRight" size={1} />
            <span className="is-size-5 has-text-grey-dark is-clickable">
                {props.text ?? NO_CONTENT}
            </span>
        </span>
    );
};

export const RenderTargetLink = (props: DataProps): React.ReactElement => {
    return <div id={props.value}>{props.text}</div>;
};

export const RenderExternalLink = (props: TextItem): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);

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
            isLoading={data.isLoading}
            isMobile={media.isMobile}
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

export const RenderInternalLink = (props: TextItem): React.ReactElement => {
    const history = useHistory();
    const media = useDimensions();

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);

    const hasImage = !Validate.isEmpty(props.propImg);
    const imageUrl = hasImage ? GET_NON_VIDEO_ASSET.replace("{name}", props.propImg!) : "";

    const onClickEvent = React.useCallback(() => {
        history.push(`/${languageId}${props.value}`);
    }, [props.value, languageId]);

    return (
        <ArticleCardView
            isLoading={data.isLoading}
            isMobile={media.isMobile}
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

export const RenderArticleLink = (props: DataProps): React.ReactElement => {
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

/* HEADER COMPONENT */

export const RenderTitle = (props: DataProps): React.ReactElement => {
    return <h1 className="bulma-title has-text-grey-dark m-0 pb-2">{props.value ?? NO_CONTENT}</h1>;
};

export const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return <h2 className="bulma-subtitle has-text-grey-dark has-text-weight-normal m-0 pb-4">{props.value ?? NO_CONTENT}</h2>;
};

export const RenderHeader1 = (props: DataProps): React.ReactElement => {
    return <h1 className="bulma-title has-text-grey-dark">{props.value ?? NO_CONTENT}</h1>;
};

export const RenderHeader2 = (props: DataProps): React.ReactElement => {
    return <h2 className="bulma-subtitle has-text-grey-dark">{props.value ?? NO_CONTENT}</h2>;
};

export const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    return (
        <h3 className="is-size-5 has-text-grey-dark has-text-weight-normal line-height-22 custom-drop-cap">
            {props.value ?? NO_CONTENT}
        </h3>
    );
};

/* PARAGRAPH COMPONENTS */

export const RenderParagraph = (props: TextItem): React.ReactElement => {
    const html = props.value as string | string[];
    const prop = props.prop as TComponent;

    const baseStyle = "is-size-5 has-text-grey-dark line-height-22";
    const classStyle = props.text === "" ? baseStyle : `${baseStyle} ${GetFontStyle(props.text)}`;

    switch (prop) {
        case "p":
            return (
                <p className={classStyle}>
                    <ProcessParagraphs html={html as string} />
                </p>
            );
        case "blockquote":
            return (
                <blockquote className={classStyle}>
                    <ProcessParagraphs html={html as string} />
                </blockquote>
            );
        case "span":
            return <span className={classStyle}>{html ?? NO_CONTENT}</span>;
        case "ul":
            return <RenderList list={html as string[]} type="ul" className={classStyle} />;
        case "ol":
            return <RenderList list={html as string[]} type="ol" className={classStyle} />;
        case "div":
            return (
                <div className={classStyle}>
                    <ProcessParagraphs html={html as string} />
                </div>
            );
        case "br":
            return <div className="my-4">&nbsp;</div>;
        default:
            return (
                <p className={classStyle}>
                    <ProcessParagraphs html={html as string} />
                </p>
            );
    }
};

export const ProcessParagraphs = (props: ProcessParagraphsProps): React.ReactElement => {
    const result: React.ReactElement[] = [];

    if (!props.html || (props.html && props.html === "")) {
        return <>{NO_CONTENT}</>;
    }

    if (!props.html.includes("__{") && !props.html.includes("}__")) {
        return <>{props.html}</>;
    }

    const array = props.html.split("__");
    if (array.length > 0) {
        array.forEach(item => {
            if (item.includes("{") && item.includes("}")) {
                try {
                    const data = JSON.parse(item) as LinkProps;
                    result.push(
                        <a key={uuidv4()} href={data.href} target={data.target} rel={data.rel}>
                            {data.text}
                        </a>
                    );
                } catch {
                    console.error(item);
                    throw "Parsing error.";
                }
            } else {
                if (!Validate.isEmpty(item)) {
                    result.push(<React.Fragment key={uuidv4()}>{item}</React.Fragment>);
                }
            }
        });
    } else {
        return <>{props.html}</>;
    }

    return <>{result}</>;
};
