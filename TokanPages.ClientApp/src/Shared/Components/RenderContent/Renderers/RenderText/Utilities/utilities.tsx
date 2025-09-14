import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { GET_NON_VIDEO_ASSET } from "../../../../../../Api/Paths";
import { ArticleInfoAction } from "../../../../../../Store/Actions";
import { ApplicationState } from "../../../../../../Store/Configuration";
import { ArticleItemBase } from "../../../Models";
import { TextItem } from "../../../Models/TextModel";
import { useHash } from "../../../../../../Shared/Hooks";
import {
    ArticleCard,
    ArticleCardView,
    Icon,
    Link,
    ProgressBar,
    RenderHtml,
    RenderList,
} from "../../../../../../Shared/Components";
import { TComponent, TLoading } from "../../../../../../Shared/types";
import { TagType } from "../../../../../../Shared/Components/RenderHtml/types";
import { TryParse } from "../../../../../../Shared/Services/Utilities";
import DOMPurify from "dompurify";
import Validate from "validate.js";
import { v4 as uuidv4 } from "uuid";
import "./utilities.css";

interface DataProps {
    value?: string;
    text?: string;
    loading?: TLoading;
}

interface LinkProps {
    href: string;
    target: string;
    rel: string;
    text: string;
}

interface ProcessParagraphsProps {
    tag: TagType;
    html: string | undefined;
    className?: string;
}

interface RenderTagProps {
    tag: TagType;
    children: React.ReactElement | React.ReactElement[];
    className?: string;
    style?: React.CSSProperties;
}

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

const GetFontStyle = (value: string): string => {
    switch (value) {
        /* TEXT WEIGHT */
        case "light":
            return "has-text-weight-light";
        case "normal":
            return "has-text-weight-normal";
        case "medium":
            return "has-text-weight-medium";
        case "semibold":
            return "has-text-weight-semibold";
        case "bold":
            return "has-text-weight-bold";
        case "extrabold":
            return "has-text-weight-extrabold";

        /* TEXT TRANSFORMATION */
        case "capitalized":
            return "is-capitalized";
        case "lowercase":
            return "is-lowercase";
        case "uppercase":
            return "is-uppercase";
        case "italic":
            return "is-italic";
        case "underlined":
            return "is-underlined";

        /* FALLBACK */
        default:
            return "";
    }
};

/* RENDER TAG HELPER */

export const RenderTag = (props: RenderTagProps) => {
    const attributes = {
        className: props.className,
        style: props.style,
    };

    switch (props.tag) {
        case "p":
            return <p {...attributes}>{props.children}</p>;
        case "span":
            return <span {...attributes}>{props.children}</span>;
        case "h1":
            return <h1 {...attributes}>{props.children}</h1>;
        case "h2":
            return <h2 {...attributes}>{props.children}</h2>;
        case "h3":
            return <h3 {...attributes}>{props.children}</h3>;
        case "h4":
            return <h4 {...attributes}>{props.children}</h4>;
        case "h5":
            return <h5 {...attributes}>{props.children}</h5>;
        case "h6":
            return <h6 {...attributes}>{props.children}</h6>;
        case "blockquote":
            return <blockquote {...attributes}>{props.children}</blockquote>;
        case "li":
            return <li {...attributes}>{props.children}</li>;
        default:
            return <div {...attributes}>{props.children}</div>;
    }
};

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
            <Icon name="MenuRight" size={1.5} />
            <span className="is-size-5 has-text-grey-dark is-clickable">{props.text ?? NO_CONTENT}</span>
        </span>
    );
};

export const RenderTargetLink = (props: DataProps): React.ReactElement => {
    return <div id={props.value}>{props.text}</div>;
};

export const RenderExternalLink = (props: TextItem): React.ReactElement => {
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
            imageUrl={imageUrl}
            title={props.propTitle ?? ""}
            description={props.propSubtitle ?? ""}
            onClickEvent={onClickEvent}
            buttonText={props.text}
            flagImage={""}
            canAnimate={false}
            loading={props.loading}
        />
    );
};

export const RenderInternalLink = (props: TextItem): React.ReactElement => {
    const history = useHistory();

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);

    const hasImage = !Validate.isEmpty(props.propImg);
    const imageUrl = hasImage ? GET_NON_VIDEO_ASSET.replace("{name}", props.propImg!) : "";

    const onClickEvent = React.useCallback(() => {
        history.push(`/${languageId}${props.value}`);
    }, [props.value, languageId]);

    return (
        <ArticleCardView
            isLoading={data.isLoading}
            imageUrl={imageUrl}
            title={props.propTitle ?? ""}
            description={props.propSubtitle ?? ""}
            onClickEvent={onClickEvent}
            buttonText={props.text}
            flagImage={""}
            canAnimate={false}
            loading={props.loading}
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
            loading={props.loading}
        />
    );
};

/* HEADER COMPONENT */

export const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <RenderHtml value={props.value ?? NO_CONTENT} tag="h2" className="bulma-title has-text-grey-dark pt-2 pb-2" />
    );
};

export const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <RenderHtml
            value={props.value ?? NO_CONTENT}
            tag="h3"
            className="bulma-subtitle has-text-grey-dark has-text-weight-normal m-0 p-0"
        />
    );
};

export const RenderHeader1 = (props: DataProps): React.ReactElement => {
    return <RenderHtml value={props.value ?? NO_CONTENT} tag="h2" className="bulma-title has-text-grey-dark" />;
};

export const RenderHeader2 = (props: DataProps): React.ReactElement => {
    return <RenderHtml value={props.value ?? NO_CONTENT} tag="h3" className="bulma-subtitle has-text-grey-dark" />;
};

export const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    return (
        <RenderHtml
            value={props.value ?? NO_CONTENT}
            tag="h4"
            className="is-size-5 has-text-grey-dark has-text-weight-normal line-height-22 custom-drop-cap"
        />
    );
};

/* PARAGRAPH COMPONENTS */

export const RenderParagraph = (props: TextItem): React.ReactElement => {
    const html = props.value as string | string[];
    const prop = props.prop as TComponent;

    const baseStyle = "is-size-5 has-text-grey-dark line-height-22";
    const classStyle = props.text === "" ? baseStyle : `${baseStyle} ${GetFontStyle(props.text)}`;

    switch (prop) {
        case "blockquote":
            return <ProcessParagraphs tag="blockquote" html={html as string} className={classStyle} />;
        case "span":
            return <RenderHtml value={html as string} tag="span" className={classStyle} />;
        case "ul":
            return <RenderList list={html as string[]} type="ul" className={classStyle} />;
        case "ol":
            return <RenderList list={html as string[]} type="ol" className={classStyle} />;
        case "div":
            return <ProcessParagraphs tag="div" html={html as string} className={classStyle} />;
        case "br":
            return <div className="my-4">&nbsp;</div>;
        default:
            return <ProcessParagraphs tag="p" html={html as string} className={classStyle} />;
    }
};

export const ProcessParagraphs = (props: ProcessParagraphsProps): React.ReactElement => {
    if (!props.html || (props.html && props.html === "")) {
        return <>{NO_CONTENT}</>;
    }

    if (!props.html.includes("__{") && !props.html.includes("}__")) {
        return <RenderHtml value={props.html} tag={props.tag} className={props.className} />;
    }

    const result: React.ReactElement[] = [];
    const array = props.html.split("__");

    if (array.length > 0) {
        array.forEach(item => {
            const sanitized = DOMPurify.sanitize(item, { ALLOWED_TAGS: ["a", "b", "i", "u"], ADD_ATTR: ["target"] });

            if (item.includes("{") && item.includes("}")) {
                const data = TryParse<LinkProps>(sanitized);
                result.push(
                    <Link to={data.href} key={uuidv4()}>
                        <>{data.text}</>
                    </Link>
                );
            } else {
                if (!Validate.isEmpty(sanitized)) {
                    result.push(<RenderHtml value={sanitized} tag="span" className={props.className} key={uuidv4()} />);
                }
            }
        });
    }

    return (
        <RenderTag tag={props.tag} className={props.className}>
            {result}
        </RenderTag>
    );
};
