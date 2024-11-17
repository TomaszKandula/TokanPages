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
import {
    Card,
    CardContent,
    CircularProgress,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    Paper,
    Checkbox,
} from "@material-ui/core";
import { DescriptionItemDto, PricingDto, ServiceItemDto, TechItemsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../Shared/types";
import { BusinessFormProps, ServiceItemCardProps, TechStackListProps } from "../Models";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    caption: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    techHandler: (event: ReactChangeEvent, isChecked: boolean) => void;
    serviceHandler: (event: ReactMouseEvent) => void;
    serviceSelection: string[];
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

interface RenderTextProps {
    isLoading: boolean;
    value: string;
    height: number;
}

interface RenderElementProps {
    isLoading: boolean;
    element: React.ReactElement | React.ReactElement[];
    height: number;
}

const ActiveButton = (props: BusinessFormViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="button"
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

const TechStackList = (props: TechStackListProps): React.ReactElement => {
    return (
        <List>
            {props.list.map((value: TechItemsDto, index: number) => (
                <ListItem
                    disabled={props.isDisabled}
                    key={value.key}
                    role={undefined}
                    button
                    className="business-list-item"
                >
                    <ListItemIcon>
                        <Checkbox
                            id={`${index}`}
                            name={`tech-${index}`}
                            edge="start"
                            onChange={props.handler}
                            checked={value.isChecked}
                            tabIndex={-1}
                            disableRipple={true}
                            inputProps={{ "aria-labelledby": `key-${index}` }}
                            className="violet-check-box"
                        />
                    </ListItemIcon>
                    <ListItemText id={`${value.key}`} primary={value.value} />
                </ListItem>
            ))}
        </List>
    );
};

const ServiceItemCard = (props: ServiceItemCardProps) => {
    const isSelected = props.services.includes(props.value.id) ?? false;
    const style = isSelected ? "business-selected" : "business-unselected";
    const disabled = props.isDisabled ? "business-disabled" : "business-enabled";
    return (
        <Grid item xs={12} sm={4}>
            <Paper
                id={props.value.id}
                data-disabled={props.isDisabled}
                elevation={0}
                className={`business-paper ${style} ${disabled}`}
                onClick={props.handler}
            >
                <Typography component="span" className="business-pricing-text">
                    <ReactHtmlParser html={props.value.text} />
                </Typography>
                <Box mt={2}>
                    <Typography component="span" className="business-pricing-text">
                        <ReactHtmlParser html={props.value.price} />
                    </Typography>
                </Box>
            </Paper>
        </Grid>
    );
};

const RenderText = (props: RenderTextProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant="rect" width="100%" height={`${props.height}px`} /> : <>{props.value}</>;
};

const RenderElement = (props: RenderElementProps): React.ReactElement => {
    return props.isLoading ? (
        <Skeleton variant="rect" width="100%" height={`${props.height}px`} />
    ) : (
        <>{props.element}</>
    );
};

export const BusinessFormView = (props: BusinessFormViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <Box pt={15} pb={30}>
                    <Box textAlign="center">
                        <Typography gutterBottom={true} className="business-large-caption">
                            {props.hasCaption ? (
                                <RenderText
                                    isLoading={props.isLoading}
                                    value={props.caption?.toUpperCase()}
                                    height={60}
                                />
                            ) : (
                                <></>
                            )}
                        </Typography>
                    </Box>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Box mb={3} textAlign="center">
                                {props.hasIcon ? (
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        element={
                                            <>
                                                <BusinessCenterIcon className="business-main-icon" />
                                                <Typography className="business-small-caption">
                                                    {props.caption}
                                                </Typography>
                                            </>
                                        }
                                        height={60}
                                    />
                                ) : (
                                    <></>
                                )}
                            </Box>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required
                                                fullWidth
                                                id="company"
                                                name="company"
                                                variant="outlined"
                                                disabled={props.progress}
                                                inputProps={{ maxLength: 255 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.companyText}
                                                label={props.companyLabel}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required
                                                fullWidth
                                                id="firstName"
                                                name="firstName"
                                                variant="outlined"
                                                disabled={props.progress}
                                                inputProps={{ maxLength: 255 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.firstNameText}
                                                label={props.firstNameLabel}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required
                                                fullWidth
                                                id="lastName"
                                                name="lastName"
                                                variant="outlined"
                                                disabled={props.progress}
                                                inputProps={{ maxLength: 255 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.lastNameText}
                                                label={props.lastNameLabel}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required
                                                fullWidth
                                                id="email"
                                                name="email"
                                                variant="outlined"
                                                disabled={props.progress}
                                                inputProps={{ maxLength: 255 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.emailText}
                                                label={props.emailLabel}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required
                                                fullWidth
                                                id="phone"
                                                name="phone"
                                                variant="outlined"
                                                disabled={props.progress}
                                                inputProps={{ maxLength: 17 }}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.phoneText}
                                                label={props.phoneLabel}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <TextField
                                                required={props.description.required}
                                                fullWidth
                                                id="description"
                                                name="description"
                                                variant="outlined"
                                                disabled={props.progress}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.description.text}
                                                label={props.description.label}
                                                multiline={props.description.multiline}
                                                minRows={props.description.rows}
                                            />
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <Box mt={2} mb={1}>
                                        <RenderElement
                                            isLoading={props.isLoading}
                                            height={45}
                                            element={
                                                <Typography className="business-header">{props.techLabel}</Typography>
                                            }
                                        />
                                    </Box>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={100}
                                        element={
                                            <TechStackList
                                                isDisabled={props.progress}
                                                list={props.techItems}
                                                handler={props.techHandler}
                                            />
                                        }
                                    />
                                </Grid>
                            </Grid>
                            <Box mt={2} mb={4}>
                                <Box mt={1} mb={4}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={45}
                                        element={
                                            <Typography component="span" className="business-header">
                                                <ReactHtmlParser html={props.pricing.caption} />
                                            </Typography>
                                        }
                                    />
                                </Box>
                                <Grid container spacing={3}>
                                    <RenderElement
                                        isLoading={props.isLoading}
                                        height={100}
                                        element={props.pricing.services.map((value: ServiceItemDto, _index: number) => (
                                            <ServiceItemCard
                                                key={value.id}
                                                value={value}
                                                isDisabled={props.progress}
                                                handler={props.serviceHandler}
                                                services={props.serviceSelection}
                                            />
                                        ))}
                                    />
                                </Grid>
                            </Box>
                            <Box mb={10} className="business-info-box">
                                <RenderElement
                                    isLoading={props.isLoading}
                                    height={45}
                                    element={
                                        <>
                                            <InfoIcon className="business-info-icon" />
                                            <Typography component="span">
                                                <ReactHtmlParser html={props.pricing.disclaimer} />
                                            </Typography>
                                        </>
                                    }
                                />
                            </Box>
                            <Box my={5}>
                                <RenderElement
                                    isLoading={props.isLoading}
                                    height={40}
                                    element={<ActiveButton {...props} />}
                                />
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
