import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import ReactHtmlParser from 'react-html-parser';
import useStyles from "./styleFeatures";

export default function Features() 
{

    const classes = useStyles();
    const content = 
    {
        caption: "Technologies",
        header: "I work primarily with",
        title1: "Languages",
        text1: "Today in my daily job I use <b>C#</b>, <b>JavaScript</b> and <b>TypeScript</b>. In the past I also used: Basic, Assembler, Turbo Pascal, Delphi, VBA.",
        title2: "Frameworks/Libraries",
        text2: "Back-End: NET Framework 4.5 (for one project), <b>NET Core</b> (since version 2.0). Front-End libraries: jQuery and <b>React/Redux</b>. CSS Frameworks: Bootstrap, Materialize, Bulma, and <b>Material-UI</b>.",
        title3: "OR/M",
        text3: "I have started with Entity Framework and did just one project, in other projects I use <b>Entity Framework Core</b>. I prefer to use lightweight OR/M like Dapper and use <b>SQL/T-SQL</b> when necessary.",
        title4: "Cloud Services",
        text4: "I have experience with <b>Azure Cloud Services</b>: App Services (PaaS), Azure Storage, Azure SQL, CosmosDb (NoSQL), WebJobs, Azure Functions (C#), Application Insights."
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box py={8}>
                        <Box mb={8}>
                            <Typography color="primary" variant="button" component="h3" align="center" gutterBottom={true}>
                                {content.caption}
                            </Typography>
                            <Typography variant="h4" component="h2" align="center">
                                {content.header}
                            </Typography>
                        </Box>
                        <Grid container spacing={6}>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <CodeIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {content.title1}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(content.text1)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <LibraryBooksIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {content.title2}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(content.text2)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <StorageIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {content.title3}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(content.text3)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>         
                                <Box mb={2} display="flex" alignItems="center">
                                    <CloudIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {content.title4}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(content.text4)}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                </div>
            </Container>
        </section>
    );

}