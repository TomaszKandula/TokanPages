import * as React from "react";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleCard, ProgressBar } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleListProps } from "../articleList";

interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    isMobile: boolean;
    articles: ArticleItem[];
}

interface RenderContentProps {
    articles: ArticleItem[];
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

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => (
    <section className={props.background}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <div className={props.isMobile ? "p-4" : "py-4"}>
                {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
            </div>
        </div>
    </section>
);
