import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";
import useStyles from "./Hooks/styleArticleDetail";
import Validate from "validate.js";
import CenteredCircularLoader from "Shared/ProgressBar/centeredCircularLoader";

interface IArticleDetail
{
    uid: string;    
}

export default function ArticleDetail(props: IArticleDetail) 
{

    const classes = useStyles();
    const content = 
    {
        footprint: "https://maindbstorage.blob.core.windows.net/tokanpages/content/articles/{UID}/text.html"
    };

    const [ article, setArticle ] = React.useState("");
    const articleUrl = content.footprint.replace("{UID}", props.uid);
    const fetchArticle = React.useCallback( async () => 
    {
        const response = await axios.get(articleUrl, {method: "get", responseType: "text"});
        setArticle(response.data);    
    }, [ articleUrl ]);

    React.useEffect( () => { fetchArticle() }, [ article, fetchArticle ] );
    const renderArticle = (text: string) => 
    {
        return(
            <div data-aos="fade-up">
                <Typography variant="body1" component="span" className={classes.typography}>
                    {ReactHtmlParser(text)}
                </Typography>
            </div>
        );
    }

    return (
        <section>
            <Container className={classes.container}>       
                <Box py={12}>
                    <Link to="/articles">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>
                    </Link> 
                    <Divider className={classes.divider} />
                    {Validate.isEmpty(article) ? <CenteredCircularLoader /> : renderArticle(article)}
                </Box>
            </Container>
        </section>
    );
}
