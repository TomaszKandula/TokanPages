import * as React from "react";
import { GET_FLAG_URL } from "../../../../Api";
import { Animated, CustomImage, Icon } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import Validate from "validate.js";
import "./articleCardView.css";

interface ArticleCardViewProps extends ViewProperties {
    isMobile: boolean;
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
    canAnimate: boolean;
    readCount?: string;
    totalLikes?: string;
    styleSmallCard?: boolean;
}

interface RenderReadCountProps {
    readCount?: string; 
}

interface RenderTotalLikesProps {
    totalLikes?: string;
}

interface RenderFlagProps {
    flagImage: string;
}

const RenderReadCount = (props: RenderReadCountProps): React.ReactElement => {
    return props.readCount === undefined ? (
        <></>
    ) : (
        <>
            <Icon name="Eye" size={1} className="mr-2" />
            <p>{props.readCount}</p>
        </>
    );
};

const RenderTotalLikes = (props: RenderTotalLikesProps): React.ReactElement => {
    return props.totalLikes === undefined ? (
        <></>
    ) : (
        <>
            <Icon name="ThumbUp" size={1} className="mx-2" />
            <p>{props.totalLikes}</p>
        </>
    );
};

const RenderFlag = (props: RenderFlagProps): React.ReactElement => {
    return !Validate.isEmpty(props.flagImage) ? (
        <>
            <Icon name="Translate" size={1} className="mx-2" />
            <CustomImage
                base={GET_FLAG_URL}
                source={props.flagImage}
                title="Articles"
                alt="An article language flag"
                className="article-flag-image"
                height={24}
                width={24}
            />
        </>
    ) : (
        <></>
    );
};

export const ArticleCardView = (props: ArticleCardViewProps): React.ReactElement => {
    const styleCard = props.styleSmallCard ? "article-card-action-small" : "article-card-action-large";
    const styleBox = props.isMobile ? "is-flex-direction-column" : "is-flex-direction-row";
    const styleImage = props.isMobile ? "article-card-image-mobile" : "article-card-image-desktop";

    return (
        <Animated isDisabled={!props.canAnimate} dataAos="fade-up">
            <div className={`bulma-box is-flex p-0 mb-6 ${styleBox}`}>
                <div className={`${props.isMobile ? "mr-0" : "mr-4"}`}>
                    <figure className="bulma-image">
                        <CustomImage 
                            source={props.imageUrl}
                            className={`article-card-image ${styleImage} lazyloaded`}
                            title="Article illustration"
                            alt="An article card for given article"
                        />
                    </figure>
                </div>
                <div className="is-flex is-flex-direction-column">
                    <div className={styleCard}>
                        <h2 className="is-size-4 has-text-black-ter">{props.title}</h2>
                        <h3 className="is-size-6 has-text-grey">{props.description}</h3>
                    </div>
                    <div className="is-flex is-justify-content-space-between">
                        <div className="is-flex is-align-items-center">
                            <RenderReadCount {...props} />
                            <RenderTotalLikes {...props} />
                            <RenderFlag {...props} />
                        </div>
                        <button onClick={props.onClickEvent} className="bulma-button bulma-is-link bulma-is-light m-2">
                            {props.buttonText}
                        </button>
                    </div>
                </div>
            </div>
        </Animated>
    );
};
