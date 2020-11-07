import React, { useCallback, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton, makeStyles } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";
import * as apiUrls from "../../Shared/apis";
import { IsEmpty } from "../../Shared/helpers"; 

interface IArticleDetail
{
    uid: string;    
}

const useStyles = makeStyles(() => (
{
    container:
    {
        maxWidth: "700px"
    },
    divider:
    {
        marginBottom: "30px"
    },
    typography:
    {
        textAlign: "justify",
        color: "#616161",
        lineHeight: 2.0
    }
}));    

export default function ArticleDetail(props: IArticleDetail) 
{

    const classes = useStyles();
    const [ article, setArticle ] = useState("");

    const articleUrl = `${apiUrls.STORAGE_URL}/content/articles/${props.uid}/text.html`;

    const fetchArticle = useCallback( async () => 
    {
        const response = await axios.get(articleUrl, {method: "get", responseType: "text"});
        setArticle(response.data);    
    }, [ articleUrl ]);

    useEffect( () => { fetchArticle() }, [ article, fetchArticle ] );

    const renderArticle = (text: string) => 
    {
        return(
            <div data-aos="fade-up">
                {ReactHtmlParser(text)}
            </div>
        );
    }

    const content = IsEmpty(article) ? "Fetching content..." : renderArticle(article);
    
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
                    <Typography variant="body1" component="span" className={classes.typography}>
                        {content}
                    </Typography>
                </Box>
            </Container>
        </section>
    );
}
