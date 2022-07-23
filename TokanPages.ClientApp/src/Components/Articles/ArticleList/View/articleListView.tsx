import * as React from "react";
import Container from "@material-ui/core/Container";
import { Box, Divider, Grid, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import { IArticleItem } from "../../../../Shared/Components/ContentRender/Models";
import { ProgressBar } from "../../../../Shared/Components";
import { ArticleCard } from "../../../Articles";
import { ArticleListStyle } from "./articleListStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    articles: IArticleItem[];
}

export const ArticleListView = (props: IBinding): JSX.Element => 
{
    const classes = ArticleListStyle();

    const RenderContent = (args: { articles: IArticleItem[] }): JSX.Element =>
    {
        return(
            <>
                {args.articles.map((item: IArticleItem) => ( 
                    <ArticleCard 
                        title={item.title}
                        description={item.description}
                        id={item.id}
                        key={item.id}
                    />
                ))}
            </>
        );
    }

    return (
        <section>
            <Container className={classes.container}>
                <Box pt={12} pb={8}>
                    <div data-aos="fade-down">
                        <Link to="/">
                            <IconButton>
                                <ArrowBack/>
                            </IconButton>
                        </Link> 
                        <Divider className={classes.divider} />
                    </div>
                    <Grid container justifyContent="center">
                        <Grid item xs={12} sm={12}>
                            {props.bind?.isLoading 
                                ? <ProgressBar /> 
                                : <RenderContent articles={props.bind?.articles} />}
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
