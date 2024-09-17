import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import BusinessCenterIcon from "@material-ui/icons/BusinessCenter";
import Skeleton from "@material-ui/lab/Skeleton";
import InfoIcon from "@material-ui/icons/Info";
import { Card, CardContent, CircularProgress, List, ListItem, ListItemIcon, ListItemText, Paper } from "@material-ui/core";
import { DescriptionItemDto, PricingDto, ServiceItemDto, TechItemsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../Shared/types";
import { VioletCheckbox } from "../../../Theme";
import { BusinessFormProps, TechStackItem, TechStackListProps } from "../Models";
import { BusinessFormStyle } from "./businessFormStyle";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    caption: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    techHandler: (value: TechItemsDto, isChecked: boolean) => void;
    serviceHandler: (event: ReactMouseEvent, id: string) => void;
    serviceSelection?: string[];
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
    techLabel: string;
    techItems: TechItemsDto[];
    description: ExtendedDescriptionProps;
    pricing: PricingDto;
}

interface ExtendedDescriptionProps extends DescriptionItemDto {
    text: string;
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
    const classes = BusinessFormStyle();
    return (
        <List>
            {props.list.map((value: TechStackItem, index: number) => (
                <ListItem key={index} role={undefined} button className={classes.list_item}>
                    <ListItemIcon>
                        <VioletCheckbox
                            id={`tech-${index}`}
                            name={`tech-${index}`}
                            edge="start"
                            onChange={(_: ReactChangeEvent, checked: boolean) => props.handler(value, checked)}
                            tabIndex={-1}
                            disableRipple={true}
                            inputProps={{ "aria-labelledby": `key-${index}` }}
                    />
                    </ListItemIcon>
                    <ListItemText id={value.key.toString()} primary={value.value} />
                </ListItem>
            ))}
        </List>
    );
}

interface ServiceItemCardProps {
    key: number;
    value: ServiceItemDto;
    handler: (event: ReactMouseEvent, id: string) => void;
    services?: string[];
}

const ServiceItemCard = (props: ServiceItemCardProps) => {
    const classes = BusinessFormStyle();
    const isSelected = props.services?.includes(props.value.id) ?? false;
    const style = isSelected ? classes.selected : classes.unselected;
    return (
        <Grid item xs={12} sm={4}>
            <Paper 
                elevation={0} 
                className={`${classes.paper} ${style}`}
                onClick={(event: ReactMouseEvent) => props.handler(event, props.value.id) }
            >
                <Typography component="span" className={classes.pricing_text}>
                    <ReactHtmlParser html={props.value.text} />
                </Typography>
                <Box mt={2}>
                    <Typography component="span" className={classes.pricing_text}>
                        <ReactHtmlParser html={props.value.price} />
                    </Typography>
                </Box>
            </Paper>
        </Grid>
    );
}

export const BusinessFormView = (props: BusinessFormViewProps): JSX.Element => {
    const classes = BusinessFormStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box pt={15} pb={30}>
                    <Box textAlign="center" data-aos="fade-down">
                        <Typography gutterBottom={true} className={classes.large_caption}>
                            {props.hasCaption ? props.caption?.toUpperCase() : <></>}
                        </Typography>
                    </Box>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                {props.hasIcon ? (
                                    <>
                                        <BusinessCenterIcon className={classes.main_icon} />
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
                                                variant="outlined"
                                                inputProps={{ maxLength: 255 }}
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
                                                id="firstName"
                                                name="firstName"
                                                variant="outlined"
                                                inputProps={{ maxLength: 255 }}
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
                                                id="lastName"
                                                name="lastName"
                                                variant="outlined"
                                                inputProps={{ maxLength: 255 }}
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
                                                variant="outlined"
                                                inputProps={{ maxLength: 255 }}
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
                                                variant="outlined"
                                                inputProps={{ maxLength: 17 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.phoneText}
                                                label={props.phoneLabel}
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
                                                required={props.description.required}
                                                fullWidth
                                                id="description"
                                                name="description"
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
                                <Grid item xs={12}>
                                    <Box mt={2} mb={1}>
                                        <Typography className={classes.header}>
                                            {props.techLabel}
                                        </Typography>
                                    </Box>
                                    <div data-aos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TechStackList list={props.techItems} handler={props.techHandler} />
                                        )}
                                    </div>
                                </Grid>
                            </Grid>
                            <Box mt={2} mb={4}>
                                <Box mt={1} mb={4}>
                                    <Typography component="span" className={classes.header}>
                                        <ReactHtmlParser html={props.pricing.caption} />
                                    </Typography>
                                </Box>
                                <Grid container spacing={3}>
                                    {props.pricing.services.map((value: ServiceItemDto, index: number) => (
                                        <ServiceItemCard 
                                            key={index} 
                                            value={value} 
                                            handler={props.serviceHandler} 
                                            services={props.serviceSelection}
                                        />
                                    ))}
                                </Grid>
                            </Box>
                            <Box mb={10} className={classes.info_box}>
                                <InfoIcon className={classes.info_icon} />
                                <Typography component="span">
                                    <ReactHtmlParser html={props.pricing.disclaimer} />
                                </Typography>
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
