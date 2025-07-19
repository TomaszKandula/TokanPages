import * as React from "react";
import { GET_FLAG_URL } from "../../../../Api";
import { ArticleContentDto } from "../../../../Api/Models";
import { Animated, Icon, CustomImage, Skeleton } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ExtendedViewProps } from "../articleDetail";
import "./articleDetailView.css";

interface ArticleDetailViewProps extends ExtendedViewProps {
    isLoading: boolean;
    isMobile: boolean;
    backButtonHandler: () => void;
    articleReadCount: string;
    renderSmallAvatar: React.ReactElement;
    renderLargeAvatar: React.ReactElement;
    authorAliasName: string;
    authorFirstName: string;
    authorLastName: string;
    authorRegistered: string;
    articleReadTime: string;
    articleCreatedAt: string;
    articleUpdatedAt: string;
    articleContent: React.ReactElement;
    renderLikesLeft: string;
    thumbsHandler: () => void;
    totalLikes: string;
    renderAuthorName: string;
    authorShortBio: string;
    flagImage: string;
    content: ArticleContentDto;
}

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => (
    <section className={props.background}>
        <div className="bulma-container bulma-is-max-tablet">
            <article className="py-4">
                <Animated dataAos="fade-down" className="pb-5">
                    <div className={`bulma-grid is-flex m-0 ${props.isMobile ? "px-4" : ""}`}>
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72} disableMarginY>
                                {props.renderSmallAvatar}
                            </Skeleton>
                        </div>
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24} disableMarginY>
                                <p className="is-size-5">{props.authorAliasName}</p>
                            </Skeleton>
                        </div>
                    </div>

                    <div className={props.isMobile ? "px-4 pt-4 pb-6" : "py-4"}>
                        <div className="bulma-grid is-flex m-0">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="bulma-cell is-align-content-center is-size-6">
                                    {props.content.textLanguage}
                                </p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Rect" width={100} height={12}>
                                <CustomImage
                                    base={GET_FLAG_URL}
                                    source={props.flagImage}
                                    title="Articles"
                                    alt="An article language flag"
                                    className="article-details-flag is-flex is-align-self-center"
                                />
                            </Skeleton>
                        </div>
                        <div className="bulma-grid is-flex m-0">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">{props.content.textReadTime1}</p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">
                                    {props.articleReadTime} {props.content.textReadTime2}
                                </p>
                            </Skeleton>
                        </div>
                        <div className="bulma-grid is-flex m-0">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">{props.content.textPublished}</p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">
                                    {GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}
                                </p>
                            </Skeleton>
                        </div>
                        <div className="bulma-grid is-flex m-0">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">{props.content.textUpdated}</p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">
                                    {GetDateTime({ value: props.articleUpdatedAt, hasTimeVisible: true })}
                                </p>
                            </Skeleton>
                        </div>
                        <div className="bulma-grid is-flex m-0">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">{props.content.textReadCount}</p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                                <p className="is-size-6">{props.articleReadCount}</p>
                            </Skeleton>
                        </div>
                    </div>
                </Animated>

                <Animated dataAos="fade-up">{props.articleContent}</Animated>

                <div className={props.isMobile ? "px-4 mt-6" : "mt-6"}>
                    <div className="bulma-grid is-flex">
                        <div className="bulma-cell is-align-content-center">
                            <Icon
                                name="ThumbUp"
                                size={1.2}
                                className="article-details-thumbup"
                                onClick={props.thumbsHandler}
                            />
                        </div>
                        <div className="bulma-cell is-align-content-center">
                            <p>{props.totalLikes}</p>
                        </div>
                    </div>
                </div>

                <hr className={props.isMobile ? "mx-4" : ""} />

                <div className={`bulma-grid is-flex mb-5 ${props.isMobile ? "px-4" : ""}`}>
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
                            <p className="is-size-6">{props.renderAuthorName}</p>
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
