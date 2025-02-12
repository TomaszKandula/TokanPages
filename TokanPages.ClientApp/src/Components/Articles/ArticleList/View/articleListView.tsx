import * as React from "react";
import Container from "@material-ui/core/Container";
import { Grid } from "@material-ui/core";
import { ArticleItem } from "../../../../Shared/Components/RenderContent/Models";
import { ArticleCard, ProgressBar } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ArticleListProps } from "../articleList";

interface ArticleListViewProps extends ViewProperties, ArticleListProps {
    articles: ArticleItem[];
}

const RenderContent = (args: { articles: ArticleItem[] }): React.ReactElement => {
    return (
        <>
            {args.articles.map((item: ArticleItem) => (
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
};

export const ArticleListView = (props: ArticleListViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container">
                <div className="pb-64">
                    <Grid container justifyContent="center">
                        <Grid item xs={12} sm={12}>
                            {props.isLoading ? <ProgressBar /> : <RenderContent articles={props.articles} />}
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
