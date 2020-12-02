import * as React from "react";
import Container from "@material-ui/core/Container";
import { Box, Divider, Grid, IconButton } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import useStyles from "./Hooks/styleArticleList";
import { useDispatch, useSelector } from "react-redux";
import { IListArticles } from "Redux/applicationState";
import { actionCreators } from "Redux/Actions/listArticlesActions";

export default function ArticleList() 
{

    const classes = useStyles();

    const data = useSelector((state: IListArticles) => state.listArticles);
    console.log(data);

    const dispatch = useDispatch();
    const fetchData = React.useCallback(() => { dispatch(actionCreators.requestArticles()); }, [dispatch]);
    React.useEffect( () => { fetchData() }, [ fetchData ] );

    return (
        <section>
            <Container className={classes.container}>       
                <Box pt={12} pb={8}>             
                    <Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>      
                    </Link> 
                    <Divider className={classes.divider} />
                    <Grid container justify="center">    				
                        <Grid item xs={12} sm={12}>


                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
