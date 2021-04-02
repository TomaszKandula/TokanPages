import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { Box, Divider, Grid, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import useStyles from "./Hooks/styleArticleList";
import { ActionCreators } from "../../Redux/Actions/listArticlesActions";
import { IArticleItem, IArticles } from "../../Redux/States/articleState";
import { IApplicationState } from "../../Redux/applicationState";
import ArticleCard from "./articleCard";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";

export default function ArticleList() 
{
    const classes = useStyles();
    const data = useSelector((state: IApplicationState) => state.listArticles);

    const dispatch = useDispatch();
    const fetchData = React.useCallback(() => 
    { 
        dispatch(ActionCreators.requestArticles()); 
    }, 
    [dispatch]);
    
    React.useEffect(() => 
    { 
        fetchData() 
    }, 
    [ fetchData ]);

    const renderContent = (data: IArticles) =>
    {
        return(
            <>
                {data.articles.map((item: IArticleItem) => ( 
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
                    <Grid container justify="center">
                        <Grid item xs={12} sm={12}>
                            {data.isLoading 
                                ? <CenteredCircularLoader /> 
                                : renderContent(data)}
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
