import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import BusinessCenterIcon from "@material-ui/icons/BusinessCenter";
import Skeleton from "@material-ui/lab/Skeleton";
import { Card, CardContent, Checkbox, CircularProgress, Divider, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { BusinessFormStyle } from "./businessFormStyle";
import { BusinessFormProps } from "../businessForm";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    caption: string;
    text: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
}

interface TechStackItem {
    value: string;
    key: number;
}

interface FormProps {
    companyText: string;
    companyLabel: string;
    firstNameText: string;
    firstNameLabel: string;
    lastNameText: string;
    lastNameLabel: string;
    emailText: string;
    emailLabel: string;
    phoneText: string;
    phoneLabel: string;
    techHeader: string;
    techItems: TechStackItem[];
    techLabel: string;
    description: DescriptionProps;
    pricing: PricingProps;
}

interface DescriptionProps {
    header: string;
    text: string;
    label: string;
    multiline: boolean;
    rows: number;
    required?: boolean | undefined;
}

interface PricingProps {
    caption: string;
    text: string;
    hosting: string;
    support: string;
}

interface TechStackListProps {
    list: TechStackItem[];
}

const ActiveButton = (props: BusinessFormViewProps): JSX.Element => {
    const classes = BusinessFormStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className={classes.button}
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

const TechStackList = (props: TechStackListProps): JSX.Element => {
    return (
        <List>
            {props.list.map((value: TechStackItem, index: number) => (
                <ListItem key={index} role={undefined} dense button>
                    <ListItemIcon>
                        <Checkbox
                            edge="start"
                            //checked={checked.indexOf(value) !== -1}
                            tabIndex={-1}
                            disableRipple
                            inputProps={{ "aria-labelledby": `key-${index}` }}
                    />
                    </ListItemIcon>
                    <ListItemText id={value.key.toString()} primary={value.value} />
                </ListItem>
            ))}
        </List>
    );
}

export const BusinessFormView = (props: BusinessFormViewProps): JSX.Element => {
    const classes = BusinessFormStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box pt={15} pb={30}>
                    <Box textAlign="center" data-aos="fade-down">
                        <Typography gutterBottom={true} className={classes.caption}>
                            {props.hasCaption ? props.caption?.toUpperCase() : <></>}
                        </Typography>
                    </Box>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                {props.hasIcon ? (
                                    <>
                                        <BusinessCenterIcon className={classes.icon} />
                                        <Typography className={classes.small_caption}>{props.caption}</Typography>
                                    </>
                                ) : (
                                    <></>
                                )}
                            </Box>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="company"
                                                name="company"
                                                autoComplete="company"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.companyText}
                                                label={props.companyLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="firstname"
                                                name="firstname"
                                                autoComplete="firstname"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.firstNameText}
                                                label={props.firstNameLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="lastname"
                                                name="lastname"
                                                autoComplete="lastname"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.lastNameText}
                                                label={props.lastNameLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="email"
                                                name="email"
                                                autoComplete="email"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.emailText}
                                                label={props.emailLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="phone"
                                                name="phone"
                                                autoComplete="phone"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.phoneText}
                                                label={props.phoneLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <Box mt={1} mb={1}>
                                        <Typography className={classes.header_text}>
                                            {props.techHeader}
                                        </Typography>
                                    </Box>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TechStackList list={props.techItems} />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <Box mt={1} mb={2}>
                                        <Typography className={classes.header_text}>
                                            {props.description.header}
                                        </Typography>
                                    </Box>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required={props.description.required}
                                                fullWidth
                                                id="web"
                                                name="web"
                                                autoComplete="web"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.text}
                                                label={props.description.label}
                                                multiline={props.description.multiline}
                                                minRows={props.description.rows}
                                            />
                                        )}
                                    </div>
                                </Grid>
                            </Grid>
                            <Box mt={5} mb={3}>
                                <Divider />
                            </Box>
                            <Box mt={2}>
                                <Typography className={classes.pricing_caption}>
                                    <ReactHtmlParser html={props.pricing.caption} />
                                </Typography>
                                <Typography className={classes.pricing_text}>
                                    <ReactHtmlParser html={props.pricing.text} />
                                </Typography>
                                <Typography className={classes.pricing_text}>
                                    <ReactHtmlParser html={props.pricing.hosting} />
                                </Typography>
                                <Typography className={classes.pricing_text}>
                                    <ReactHtmlParser html={props.pricing.support} />
                                </Typography>
                            </Box>
                            <Box mt={5} mb={3}>
                                <Divider />
                            </Box>
                            <Box my={5} data-aos="fade-up">
                                {props.isLoading ? (
                                    <Skeleton variant="rect" width="100%" height="40px" />
                                ) : (
                                    <ActiveButton {...props} />
                                )}
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
