import * as React from "react";
import { GET_IMAGES_URL } from "../../../../Api";
import { ArticleContentDto } from "../../../../Api/Models";
import { Icon, CustomImage, Skeleton, Media } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ExtendedViewProps } from "../articleDetail";
import { v4 as uuidv4 } from "uuid";
import "./articleDetailView.css";

interface ArticleDetailViewProps extends ExtendedViewProps {
    isLoading: boolean;
    isMobile: boolean;
    backButtonHandler: () => void;
    articleReadCount: string;
    renderSmallAvatar: React.ReactElement;
    renderLargeAvatar: React.ReactElement;
    authorFirstName: string;
    authorLastName: string;
    authorRegistered: string;
    articleTags?: string[];
    articleReadTime: string;
    articleCreatedAt: string;
    articleUpdatedAt: string;
    articleContent: React.ReactElement;
    renderLikesLeft: string;
    thumbsHandler: () => void;
    totalLikes: string;
    authorShortBio: string;
    flagImage: string;
    content: ArticleContentDto;
}

const RenderTopHeader = (props: ArticleDetailViewProps): React.ReactElement => (
    <div className="is-flex is-gap-1.5 is-align-items-center">
        <Skeleton isLoading={props.isLoading} mode="Text" width={75} height={24}>
            <p className="bulma-cell is-align-content-center is-size-7">{props.content.textLanguage}</p>
        </Skeleton>
        <Skeleton isLoading={props.isLoading} mode="Circle" width={24} height={24}>
            <CustomImage
                base={GET_IMAGES_URL}
                source={props.flagImage}
                title="Articles"
                alt="An article language flag"
                className="article-details-flag is-flex is-align-self-center is-round-border"
            />
        </Skeleton>
        <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
            <p className="is-size-7">{props.content.textReadTime1}</p>
            <p className="is-size-7">
                {props.articleReadTime} {props.content.textReadTime2}
            </p>
        </Skeleton>
        <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
            <p className="is-size-7">{props.content.textPublished}</p>
            <p className="is-size-7">{GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}</p>
        </Skeleton>
        <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
            <p className="is-size-7">{props.content.textReadCount}</p>
            <p className="is-size-7">{props.articleReadCount}</p>
        </Skeleton>
    </div>
);

const RenderTopHeaderMobile = (props: ArticleDetailViewProps): React.ReactElement => (
    <div className="is-flex is-flex-direction-column">
        <div className="is-flex is-gap-1.5 is-align-items-center">
            <Skeleton isLoading={props.isLoading} mode="Text" width={75} height={24}>
                <p className="bulma-cell is-align-content-center is-size-7">{props.content.textLanguage}</p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Circle" width={24} height={24}>
                <CustomImage
                    base={GET_IMAGES_URL}
                    source={props.flagImage}
                    title="Articles"
                    alt="An article language flag"
                    className="article-details-flag is-flex is-align-self-center is-round-border"
                />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
                <p className="is-size-7">{props.content.textReadTime1}</p>
                <p className="is-size-7">
                    {props.articleReadTime} {props.content.textReadTime2}
                </p>
            </Skeleton>
        </div>
        <div className="is-flex is-gap-1.5 is-align-items-center">
            <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
                <p className="is-size-7">{props.content.textPublished}</p>
                <p className="is-size-7">{GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}</p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24}>
                <p className="is-size-7">{props.content.textReadCount}</p>
                <p className="is-size-7">{props.articleReadCount}</p>
            </Skeleton>
        </div>
    </div>
);

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <article className="py-4">
                <div className={props.isMobile ? "mx-4 mb-5" : "mb-5"}>
                    <div className="is-flex is-flex-direction-column">
                        <div className="is-flex is-gap-1.5 is-align-items-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" width={48} height={48} disableMarginY>
                                {props.renderSmallAvatar}
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={220} height={24} disableMarginY>
                                <p className="is-size-6">
                                    {props.authorFirstName} {props.authorLastName}
                                </p>
                            </Skeleton>
                        </div>
                        <hr className="m-0 my-2" />
                        <Media.MobileOnly>
                            <RenderTopHeaderMobile {...props} />
                        </Media.MobileOnly>
                        <Media.TabletOnly>
                            <RenderTopHeader {...props} />
                        </Media.TabletOnly>
                        <Media.DesktopOnly>
                            <RenderTopHeader {...props} />
                        </Media.DesktopOnly>
                        <hr className="m-0 my-2" />
                    </div>
                </div>

                {props.articleContent}

                <div className={props.isMobile ? "mx-4 mt-6" : "mt-6"}>
                    <div className="bulma-tags pb-6">
                        <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                            {props.articleTags?.map((value: string, _index: number) => (
                                <div key={uuidv4()} className="bulma-tag bulma-is-light bulma-is-medium">
                                    {value}
                                </div>
                            ))}
                        </Skeleton>
                    </div>
                    <div className="bulma-grid is-flex">
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" height={24} width={24}>
                                <Icon
                                    name="ThumbUp"
                                    size={1.2}
                                    className="article-details-thumbup"
                                    onClick={props.thumbsHandler}
                                />
                            </Skeleton>
                        </div>
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" height={24} width={24}>
                                <p>{props.totalLikes}</p>
                            </Skeleton>
                        </div>
                    </div>
                </div>

                <hr className={props.isMobile ? "mx-4" : ""} />

                <div className={`bulma-grid is-flex mb-5 ${props.isMobile ? "mx-4" : ""}`}>
                    <div className="bulma-cell is-align-content-center">
                        <Skeleton isLoading={props.isLoading} mode="Circle" width={98} height={98} disableMarginY>
                            {props.renderLargeAvatar}
                        </Skeleton>
                    </div>
                    <div className="bulma-cell is-align-content-center">
                        <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                            <p className="is-size-6 has-text-weight-bold">{props.content.textWritten}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                            <p className="is-size-6">
                                {props.authorFirstName} {props.authorLastName}
                            </p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                            <p className="is-size-6">
                                {props.content.textRegistered}&nbsp;{props.authorRegistered}
                            </p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                            <p className="is-size-6">
                                {props.content.textAbout}&nbsp;{props.authorShortBio}
                            </p>
                        </Skeleton>
                    </div>
                </div>
            </article>
        </div>
    </section>
);
