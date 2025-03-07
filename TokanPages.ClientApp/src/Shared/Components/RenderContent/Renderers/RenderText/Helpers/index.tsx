import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ArrowRight } from "@material-ui/icons";
import { GET_NON_VIDEO_ASSET } from "../../../../../../Api/Request/Paths";
import { ArticleInfoAction } from "../../../../../../Store/Actions";
import { ApplicationState } from "../../../../../../Store/Configuration";
import { ArticleItemBase } from "../../../Models";
import { TextItem } from "../../../Models/TextModel";
import { useHash } from "../../../../../../Shared/Hooks";
import { ArticleCard, ArticleCardView, ProgressBar, RenderList } from "../../../../../../Shared/Components";
import { TComponent } from "../../../../../../Shared/types";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

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
        <span className="render-text-wrapper" onClick={onClickHandler}>
            <ArrowRight />
            <span className="render-text-common render-text-paragraph render-text-link">
                {props.text ?? NO_CONTENT}
            </span>
        </span>
    );
};

export const RenderTargetLink = (props: DataProps): React.ReactElement => {
    return <div id={props.value}>{props.text}</div>;
}

export const RenderExternalLink = (props: TextItem): React.ReactElement => {
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

export const RenderInternalLink = (props: TextItem): React.ReactElement => {
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

export const RenderTitle = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-title mt-56 mb-8">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

export const RenderSubtitle = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-sub-title mt-8 mb-40">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

export const RenderHeader = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-header mt-56 mb-15">
            {props.value ?? NO_CONTENT}
        </p>
    );
};

export const ProcessParagraphs = (props: ProcessParagraphsProps): React.ReactElement => {
    const result: React.ReactElement[] = [];

    if (!props.html || (props.html && props.html === "")) {
        return <>{NO_CONTENT}</>;
    }

    const array = props.html.split("__");
    if (array.length > 0) {
        array.forEach(item => {
            if (item.includes("{") && item.includes("}")) {
                try {
                    const data = JSON.parse(item) as LinkProps;
                    result.push(<a key={uuidv4()} href={data.href} target={data.target} rel={data.rel}>{data.text}</a>);
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
}

export const RenderParagraph = (props: TextItem): React.ReactElement => {
    const html = props.value as string | string[];
    const prop = props.prop as TComponent;

    const baseStyle = "render-text-common render-text-paragraph";
    const classStyle = props.text === "" ? baseStyle : `${baseStyle} render-text-${props.text}`;

    switch(prop) {
        case "p": return <p className={classStyle}><ProcessParagraphs html={html as string} /></p>;
        case "span": return <span className={classStyle}>{html ?? NO_CONTENT}</span>;
        case "ul": return <RenderList list={html as string[]} type="ul" className={classStyle} />;
        case "ol": return <RenderList list={html as string[]} type="ol" className={classStyle} />;
        case "div": return <div className={classStyle}><ProcessParagraphs html={html as string} /></div>;
        case "br": return <div className="mt-15 mb-15">&nbsp;</div>;
        default: return <p className={classStyle}><ProcessParagraphs html={html as string} /></p>;
    }
};

export const RenderParagraphWithDropCap = (props: DataProps): React.ReactElement => {
    return (
        <p className="render-text-common render-text-paragraph custom-drop-cap">
            {props.value ?? NO_CONTENT}
        </p>
    );
};
