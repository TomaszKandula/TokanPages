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
import * as apiUrls from "../../Shared/apis";
import Validate from "validate.js";

interface IArticleDetail
{
    uid: string;    
}

export default function ArticleDetail(props: IArticleDetail) 
{

    const classes = useStyles();
    const [ article, setArticle ] = React.useState("");

    const articleUrl = `${apiUrls.STORAGE_URL}/content/articles/${props.uid}/text.html`;

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
                {ReactHtmlParser(text)}
            </div>
        );
    }

    const content = Validate.isEmpty(article) ? "Fetching content..." : renderArticle(article);
    
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
