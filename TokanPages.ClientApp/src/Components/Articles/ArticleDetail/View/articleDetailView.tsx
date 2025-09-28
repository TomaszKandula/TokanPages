import * as React from "react";
import { GET_FLAG_URL } from "../../../../Api";
import { ArticleContentDto } from "../../../../Api/Models";
import { Icon, CustomImage, Skeleton } from "../../../../Shared/Components";
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

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <article className="py-4">
                <div className="mb-6">
                    <div className={`bulma-grid is-flex m-0 ${props.isMobile ? "mx-4" : ""}`}>
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72} disableMarginY>
                                {props.renderSmallAvatar}
                            </Skeleton>
                        </div>
                        <div className="bulma-cell is-align-content-center">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={24} disableMarginY>
                                <p className="is-size-4">
                                    {props.authorFirstName} {props.authorLastName}
                                </p>
                            </Skeleton>
                        </div>
                    </div>

                    <div className={props.isMobile ? "mx-4 mt-4 mb-6" : "my-4"}>
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
                                    className="article-details-flag is-flex is-align-self-center is-round-border"
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
                </div>

                {props.articleContent}

                <div className="bulma-tags pt-6 pb-3">
                    <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                        {props.articleTags?.map((value: string, _index: number) => (
                            <div key={uuidv4()} className="bulma-tag bulma-is-light bulma-is-medium">
                                {value}
                            </div>
                        ))}
                    </Skeleton>
                </div>

                <div className={props.isMobile ? "mx-4 mt-6" : "mt-6"}>
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
