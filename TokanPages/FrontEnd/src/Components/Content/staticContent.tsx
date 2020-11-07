import React, { useCallback, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton, makeStyles } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from "react-html-parser";
import axios from "axios";
import { IsEmpty } from "../../Shared/helpers"; 

interface IStoryContent
{
    dataUrl: string;
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

export default function StaticContent(props: IStoryContent) 
{

    const classes = useStyles();
    const [ data, setData ] = useState("");

    const fetchData = useCallback( async () => 
    {
        const response = await axios.get(props.dataUrl, {method: "get", responseType: "text"});
        setData(response.data);    
    }, 
    [ props.dataUrl ]);

    useEffect( () => { fetchData() }, [ data, fetchData ] );

    const renderData = (text: string) => 
    {
        return(
            <div data-aos="fade-up">
                {ReactHtmlParser(text)}
            </div>
        );
    }

    const content = IsEmpty(data) ? "Fetching content..." : renderData(data);

    return (
        <section>
            <Container className={classes.container}>       
                <Box py={12}>
                    <Link to="/">
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
