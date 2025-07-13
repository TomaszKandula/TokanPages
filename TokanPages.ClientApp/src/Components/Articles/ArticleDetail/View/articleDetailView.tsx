import * as React from "react";
import { GET_FLAG_URL } from "../../../../Api";
import { ArticleContentDto } from "../../../../Api/Models";
import { Animated, Icon, CustomImage } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ExtendedViewProps } from "../articleDetail";
import "./articleDetailView.css";

interface ArticleDetailViewProps extends ExtendedViewProps {
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

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-tablet">
                <article className="py-4">
                    <Animated dataAos="fade-down">
                        <div className="bulma-grid is-flex m-0">
                            <div className="bulma-cell is-align-content-center">{props.renderSmallAvatar}</div>
                            <div className="bulma-cell is-align-content-center">
                                <p className="is-size-5">{props.authorAliasName}</p>
                            </div>
                        </div>

                        <div className="my-4">
                            <div className="bulma-grid is-flex m-0">
                                <p className="bulma-cell is-align-content-center is-size-6">
                                    {props.content.textLanguage}
                                </p>
                                <CustomImage
                                    base={GET_FLAG_URL}
                                    source={props.flagImage}
                                    title="Articles"
                                    alt="An article language flag"
                                    className="article-details-flag is-flex is-align-self-center"
                                />
                            </div>
                            <div className="bulma-grid is-flex m-0">
                                <p className="is-size-6">{props.content.textReadTime1}</p>
                                <p className="is-size-6">
                                    {props.articleReadTime} {props.content.textReadTime2}
                                </p>
                            </div>
                            <div className="bulma-grid is-flex m-0">
                                <p className="is-size-6">{props.content.textPublished}</p>
                                <p className="is-size-6">
                                    {GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}
                                </p>
                            </div>
                            <div className="bulma-grid is-flex m-0">
                                <p className="is-size-6">{props.content.textUpdated}</p>
                                <p className="is-size-6">
                                    {GetDateTime({ value: props.articleUpdatedAt, hasTimeVisible: true })}
                                </p>
                            </div>
                            <div className="bulma-grid is-flex m-0">
                                <p className="is-size-6">{props.content.textReadCount}</p>
                                <p className="is-size-6">{props.articleReadCount}</p>
                            </div>
                        </div>
                    </Animated>

                    <Animated dataAos="fade-up">{props.articleContent}</Animated>

                    <div className="mt-6">
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

                    <hr />

                    <div className="bulma-grid is-flex mb-5">
                        <div className="bulma-cell is-align-content-center">{props.renderLargeAvatar}</div>
                        <div className="bulma-cell is-align-content-center">
                            <p className="is-size-6 has-text-weight-bold">{props.content.textWritten}</p>
                            <p className="is-size-6">{props.renderAuthorName}</p>
                            <p className="is-size-6">
                                {props.content.textRegistered}&nbsp;{props.authorRegistered}
                            </p>
                            <p className="is-size-6">
                                {props.content.textAbout}&nbsp;{props.authorShortBio}
                            </p>
                        </div>
                    </div>
                </article>
            </div>
        </section>
    );
};
