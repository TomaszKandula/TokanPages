import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import BusinessCenterIcon from "@material-ui/icons/BusinessCenter";
import { Card, CardContent, Checkbox, CircularProgress, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../Shared/Abstractions";
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
    contactText: string;
    contactLabel: string;
    emailText: string;
    emailLabel: string;
    phoneText: string;
    phoneLabel: string;
    techHeader: string;
    techItems: TechStackItem[];
    techLabel: string;
    description: DescriptionProps;
}

interface DescriptionProps {
    frontendText: string;
    frontendLabel: string;
    frontendMultiline: boolean;
    frontendRows: number;
    backendText: string;
    backendLabel: string;
    backendMultiline: boolean;
    backendRows: number;
    mobileText: string;
    mobileLabel: string;
    mobileMultiline: boolean;
    mobileRows: number;
    infoText: string;
    infoLabel: string;
    infoMultiline: boolean;
    infoRows: number;
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

const TechStackList = (props: { list: TechStackItem[] }): JSX.Element => {
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

//TODO: change view //import BusinessCenterIcon from '@material-ui/icons/BusinessCenter';
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
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="contact"
                                                name="contact"
                                                autoComplete="contact"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.contactText}
                                                label={props.contactLabel}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
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
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="subject"
                                                name="subject"
                                                autoComplete="subject"
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
                                    <Typography>
                                        {props.techHeader}
                                    </Typography>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TechStackList list={props.techItems} />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="frontend"
                                                name="frontend"
                                                autoComplete="frontend"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.frontendText}
                                                label={props.description.frontendLabel}
                                                multiline={props.description.frontendMultiline}
                                                minRows={props.description.frontendRows}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="backend"
                                                name="backend"
                                                autoComplete="backend"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.backendText}
                                                label={props.description.backendLabel}
                                                multiline={props.description.backendMultiline}
                                                minRows={props.description.backendRows}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="mobile"
                                                name="mobile"
                                                autoComplete="mobile"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.mobileText}
                                                label={props.description.mobileLabel}
                                                multiline={props.description.mobileMultiline}
                                                minRows={props.description.mobileRows}
                                            />
                                        )}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="info"
                                                name="info"
                                                autoComplete="info"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.infoText}
                                                label={props.description.infoLabel}
                                                multiline={props.description.infoMultiline}
                                                minRows={props.description.infoRows}
                                            />
                                        )}
                                    </div>
                                </Grid>
                            </Grid>
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
