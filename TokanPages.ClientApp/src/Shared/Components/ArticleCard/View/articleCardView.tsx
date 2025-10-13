import * as React from "react";
import { GET_IMAGES_URL } from "../../../../Api";
import { Animated, CustomImage, Icon, Media } from "../../../../Shared/Components";
import { TLoading, ViewProperties } from "../../../../Shared/types";
import Validate from "validate.js";
import "./articleCardView.css";

interface ArticleCardViewProps extends ViewProperties {
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
    canAnimate: boolean;
    canDisplayDate: boolean;
    published: string;
    readCount?: string;
    totalLikes?: string;
    loading?: TLoading;
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
            <Icon name="Eye" size={1.5} className="mr-2" />
            <p className="m-0">{props.readCount}</p>
        </>
    );
};

const RenderTotalLikes = (props: RenderTotalLikesProps): React.ReactElement => {
    return props.totalLikes === undefined ? (
        <></>
    ) : (
        <>
            <Icon name="ThumbUp" size={1.5} className="mx-2" />
            <p className="m-0">{props.totalLikes}</p>
        </>
    );
};

const RenderFlag = (props: RenderFlagProps): React.ReactElement => {
    return !Validate.isEmpty(props.flagImage) ? (
        <>
            <Icon name="Translate" size={1.5} className="mx-2" />
            <CustomImage
                base={GET_IMAGES_URL}
                source={props.flagImage}
                title="Articles"
                alt="An article language flag"
                className="article-flag-image is-round-border"
                height={24}
                width={24}
            />
        </>
    ) : (
        <></>
    );
};

export const ArticleCardView = (props: ArticleCardViewProps): React.ReactElement => (
    <Animated isDisabled={!props.canAnimate} dataAos="fade-up">
        <div className="bulma-box is-flex p-0 mb-6 article-box-card">
            <figure className="bulma-image">
                <CustomImage
                    source={props.imageUrl}
                    className="article-box-image"
                    title="Article illustration"
                    alt="An article card for given article"
                    loading={props.loading}
                />
            </figure>
            <div className="article-box-content">
                <div className="article-box-content-text">
                    <p className="is-size-4 has-text-black-ter mb-0">{props.title}</p>
                    <Media.DesktopOnly>
                        <p className="is-size-6 has-text-grey has-text-weight-normal m-0">{props.description}</p>
                    </Media.DesktopOnly>
                    <Media.TabletOnly>
                        <p className="is-size-6 has-text-grey has-text-weight-normal m-0">{props.description}</p>
                    </Media.TabletOnly>
                    {props.canDisplayDate ? (
                        <div className="is-flex is-align-items-center mt-2">
                            <Icon name="CalendarMonth" size={1.5} />
                            <p className="is-size-6 has-text-grey has-text-weight-normal m-0">{props.published}</p>
                        </div>
                    ) : (
                        <></>
                    )}
                </div>
                <div className="is-flex is-justify-content-space-between">
                    <div className="is-flex is-align-items-center">
                        <RenderReadCount {...props} />
                        <RenderTotalLikes {...props} />
                        <RenderFlag {...props} />
                    </div>
                    <button onClick={props.onClickEvent} className="bulma-button bulma-is-link bulma-is-light">
                        {props.buttonText}
                    </button>
                </div>
            </div>
        </div>
    </Animated>
);
