import * as React from "react";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleCard, ProgressBar, Skeleton } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleListProps } from "../articleList";
import { v4 as uuidv4 } from "uuid";

interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    isMobile: boolean;
    articles: ArticleItem[];
    title: string;
    content: string[];
}

interface RenderContentProps {
    articles: ArticleItem[];
}

interface RenderStaticTextProps {
    isLoading: boolean;
    title: string;
    text: string[];
}

const RenderContent = (props: RenderContentProps): React.ReactElement => (
    <>
        {props.articles.map((item: ArticleItem) => (
            <ArticleCard
                title={item.title}
                description={item.description}
                id={item.id}
                key={item.id}
                languageIso={item.languageIso}
                canAnimate={true}
                readCount={item.readCount}
                totalLikes={item.totalLikes}
            />
        ))}
    </>
);

const RenderStaticText = (props: RenderStaticTextProps): React.ReactElement => (
    <div className="bulma-card mb-6">
        <div className="bulma-card-content">
            <div className="bulma-content">
                <Skeleton isLoading={props.isLoading} mode="Text" height={32}>
                    <h1 className="is-size-4">{props.title}</h1>
                </Skeleton>
                <hr />
                {props.text.map((value: string, _index: number) => (
                    <Skeleton isLoading={props.isLoading} mode="Text" key={uuidv4()}>
                        <p className="is-size-6 has-text-grey line-height-18">{value}</p>
                    </Skeleton>
                ))}
            </div>
        </div>
    </div>
);

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <div className={props.isMobile ? "p-4" : "py-4"}>
                <RenderStaticText isLoading={props.isLoading} title={props.title} text={props.content} />
                {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
            </div>
        </div>
    </section>
);
