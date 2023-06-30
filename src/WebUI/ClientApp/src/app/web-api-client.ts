/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.9.4.0 (NJsonSchema v10.3.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface IDoubleScoreClient {
    get(doubleMatchIdEncoded: string | null): Observable<ScoreDto[]>;
    update(command: UpdateDoubleMatchScoreCommand): Observable<boolean>;
}

@Injectable({
    providedIn: 'root'
})
export class DoubleScoreClient implements IDoubleScoreClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    get(doubleMatchIdEncoded: string | null): Observable<ScoreDto[]> {
        let url_ = this.baseUrl + "/api/DoubleScore/{doubleMatchIdEncoded}";
        if (doubleMatchIdEncoded === undefined || doubleMatchIdEncoded === null)
            throw new Error("The parameter 'doubleMatchIdEncoded' must be defined.");
        url_ = url_.replace("{doubleMatchIdEncoded}", encodeURIComponent("" + doubleMatchIdEncoded));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet(<any>response_);
                } catch (e) {
                    return <Observable<ScoreDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<ScoreDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processGet(response: HttpResponseBase): Observable<ScoreDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ScoreDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ScoreDto[]>(<any>null);
    }

    update(command: UpdateDoubleMatchScoreCommand): Observable<boolean> {
        let url_ = this.baseUrl + "/api/DoubleScore";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdate(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processUpdate(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }
}

export interface IMatchClient {
    getAllMatches(teamMatchIdEncoded: string | null | undefined): Observable<OverviewDto>;
}

@Injectable({
    providedIn: 'root'
})
export class MatchClient implements IMatchClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getAllMatches(teamMatchIdEncoded: string | null | undefined): Observable<OverviewDto> {
        let url_ = this.baseUrl + "/api/Match?";
        if (teamMatchIdEncoded !== undefined && teamMatchIdEncoded !== null)
            url_ += "TeamMatchIdEncoded=" + encodeURIComponent("" + teamMatchIdEncoded) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAllMatches(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAllMatches(<any>response_);
                } catch (e) {
                    return <Observable<OverviewDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<OverviewDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetAllMatches(response: HttpResponseBase): Observable<OverviewDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = OverviewDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<OverviewDto>(<any>null);
    }
}

export interface IScoresClient {
    get(matchIdEncoded: string | null): Observable<ScoreDto[]>;
    update(command: UpdateMatchScoreCommand): Observable<boolean>;
}

@Injectable({
    providedIn: 'root'
})
export class ScoresClient implements IScoresClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    get(matchIdEncoded: string | null): Observable<ScoreDto[]> {
        let url_ = this.baseUrl + "/api/Scores/{matchIdEncoded}";
        if (matchIdEncoded === undefined || matchIdEncoded === null)
            throw new Error("The parameter 'matchIdEncoded' must be defined.");
        url_ = url_.replace("{matchIdEncoded}", encodeURIComponent("" + matchIdEncoded));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet(<any>response_);
                } catch (e) {
                    return <Observable<ScoreDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<ScoreDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processGet(response: HttpResponseBase): Observable<ScoreDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ScoreDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ScoreDto[]>(<any>null);
    }

    update(command: UpdateMatchScoreCommand): Observable<boolean> {
        let url_ = this.baseUrl + "/api/Scores";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdate(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processUpdate(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }
}

export interface ITeamMatchClient {
    create(command: CreateTeamMatchCommand): Observable<string>;
    get(): Observable<TeamMatchDto[]>;
    getSingle(teamMatchIdEncoded: string | null | undefined): Observable<TeamMatchDto>;
}

@Injectable({
    providedIn: 'root'
})
export class TeamMatchClient implements ITeamMatchClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    create(command: CreateTeamMatchCommand): Observable<string> {
        let url_ = this.baseUrl + "/api/TeamMatch";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreate(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processCreate(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<string>(<any>null);
    }

    get(): Observable<TeamMatchDto[]> {
        let url_ = this.baseUrl + "/api/TeamMatch";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet(<any>response_);
                } catch (e) {
                    return <Observable<TeamMatchDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<TeamMatchDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processGet(response: HttpResponseBase): Observable<TeamMatchDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(TeamMatchDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TeamMatchDto[]>(<any>null);
    }

    getSingle(teamMatchIdEncoded: string | null | undefined): Observable<TeamMatchDto> {
        let url_ = this.baseUrl + "/api/TeamMatch/singleTeamMatch?";
        if (teamMatchIdEncoded !== undefined && teamMatchIdEncoded !== null)
            url_ += "teamMatchIdEncoded=" + encodeURIComponent("" + teamMatchIdEncoded) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSingle(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSingle(<any>response_);
                } catch (e) {
                    return <Observable<TeamMatchDto>><any>_observableThrow(e);
                }
            } else
                return <Observable<TeamMatchDto>><any>_observableThrow(response_);
        }));
    }

    protected processGetSingle(response: HttpResponseBase): Observable<TeamMatchDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = TeamMatchDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TeamMatchDto>(<any>null);
    }
}

export class ScoreDto implements IScoreDto {
    scoreIdEncoded?: string;
    hostPoints?: number;
    guestPoints?: number;

    constructor(data?: IScoreDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.scoreIdEncoded = _data["scoreIdEncoded"];
            this.hostPoints = _data["hostPoints"];
            this.guestPoints = _data["guestPoints"];
        }
    }

    static fromJS(data: any): ScoreDto {
        data = typeof data === 'object' ? data : {};
        let result = new ScoreDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["scoreIdEncoded"] = this.scoreIdEncoded;
        data["hostPoints"] = this.hostPoints;
        data["guestPoints"] = this.guestPoints;
        return data; 
    }
}

export interface IScoreDto {
    scoreIdEncoded?: string;
    hostPoints?: number;
    guestPoints?: number;
}

export class UpdateDoubleMatchScoreCommand implements IUpdateDoubleMatchScoreCommand {
    doubleMatchIdEncoded?: string;
    scoreDtos?: ScoreDto[];

    constructor(data?: IUpdateDoubleMatchScoreCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.doubleMatchIdEncoded = _data["doubleMatchIdEncoded"];
            if (Array.isArray(_data["scoreDtos"])) {
                this.scoreDtos = [] as any;
                for (let item of _data["scoreDtos"])
                    this.scoreDtos!.push(ScoreDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): UpdateDoubleMatchScoreCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateDoubleMatchScoreCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["doubleMatchIdEncoded"] = this.doubleMatchIdEncoded;
        if (Array.isArray(this.scoreDtos)) {
            data["scoreDtos"] = [];
            for (let item of this.scoreDtos)
                data["scoreDtos"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IUpdateDoubleMatchScoreCommand {
    doubleMatchIdEncoded?: string;
    scoreDtos?: ScoreDto[];
}

export class OverviewDto implements IOverviewDto {
    overviewSingleMatchDtos?: OverviewSingleMatchDto[];
    overviewDoubleMatchDtos?: OverviewDoubleMatchDto[];

    constructor(data?: IOverviewDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["overviewSingleMatchDtos"])) {
                this.overviewSingleMatchDtos = [] as any;
                for (let item of _data["overviewSingleMatchDtos"])
                    this.overviewSingleMatchDtos!.push(OverviewSingleMatchDto.fromJS(item));
            }
            if (Array.isArray(_data["overviewDoubleMatchDtos"])) {
                this.overviewDoubleMatchDtos = [] as any;
                for (let item of _data["overviewDoubleMatchDtos"])
                    this.overviewDoubleMatchDtos!.push(OverviewDoubleMatchDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): OverviewDto {
        data = typeof data === 'object' ? data : {};
        let result = new OverviewDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.overviewSingleMatchDtos)) {
            data["overviewSingleMatchDtos"] = [];
            for (let item of this.overviewSingleMatchDtos)
                data["overviewSingleMatchDtos"].push(item.toJSON());
        }
        if (Array.isArray(this.overviewDoubleMatchDtos)) {
            data["overviewDoubleMatchDtos"] = [];
            for (let item of this.overviewDoubleMatchDtos)
                data["overviewDoubleMatchDtos"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IOverviewDto {
    overviewSingleMatchDtos?: OverviewSingleMatchDto[];
    overviewDoubleMatchDtos?: OverviewDoubleMatchDto[];
}

export class OverviewSingleMatchDto implements IOverviewSingleMatchDto {
    matchIdEncoded?: string;
    hostPlayerName?: string;
    guestPlayerName?: string;
    hostPoints?: number;
    guestPoints?: number;
    scoresDtos?: ScoreDto[];

    constructor(data?: IOverviewSingleMatchDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.matchIdEncoded = _data["matchIdEncoded"];
            this.hostPlayerName = _data["hostPlayerName"];
            this.guestPlayerName = _data["guestPlayerName"];
            this.hostPoints = _data["hostPoints"];
            this.guestPoints = _data["guestPoints"];
            if (Array.isArray(_data["scoresDtos"])) {
                this.scoresDtos = [] as any;
                for (let item of _data["scoresDtos"])
                    this.scoresDtos!.push(ScoreDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): OverviewSingleMatchDto {
        data = typeof data === 'object' ? data : {};
        let result = new OverviewSingleMatchDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["matchIdEncoded"] = this.matchIdEncoded;
        data["hostPlayerName"] = this.hostPlayerName;
        data["guestPlayerName"] = this.guestPlayerName;
        data["hostPoints"] = this.hostPoints;
        data["guestPoints"] = this.guestPoints;
        if (Array.isArray(this.scoresDtos)) {
            data["scoresDtos"] = [];
            for (let item of this.scoresDtos)
                data["scoresDtos"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IOverviewSingleMatchDto {
    matchIdEncoded?: string;
    hostPlayerName?: string;
    guestPlayerName?: string;
    hostPoints?: number;
    guestPoints?: number;
    scoresDtos?: ScoreDto[];
}

export class OverviewDoubleMatchDto implements IOverviewDoubleMatchDto {
    matchIdEncoded?: string;
    hostLeftPlayerName?: string;
    hostRightPlayerName?: string;
    guestLeftPlayerName?: string;
    guestRightPlayerName?: string;
    hostPoints?: number;
    guestPoints?: number;
    scoresDtos?: ScoreDto[];

    constructor(data?: IOverviewDoubleMatchDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.matchIdEncoded = _data["matchIdEncoded"];
            this.hostLeftPlayerName = _data["hostLeftPlayerName"];
            this.hostRightPlayerName = _data["hostRightPlayerName"];
            this.guestLeftPlayerName = _data["guestLeftPlayerName"];
            this.guestRightPlayerName = _data["guestRightPlayerName"];
            this.hostPoints = _data["hostPoints"];
            this.guestPoints = _data["guestPoints"];
            if (Array.isArray(_data["scoresDtos"])) {
                this.scoresDtos = [] as any;
                for (let item of _data["scoresDtos"])
                    this.scoresDtos!.push(ScoreDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): OverviewDoubleMatchDto {
        data = typeof data === 'object' ? data : {};
        let result = new OverviewDoubleMatchDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["matchIdEncoded"] = this.matchIdEncoded;
        data["hostLeftPlayerName"] = this.hostLeftPlayerName;
        data["hostRightPlayerName"] = this.hostRightPlayerName;
        data["guestLeftPlayerName"] = this.guestLeftPlayerName;
        data["guestRightPlayerName"] = this.guestRightPlayerName;
        data["hostPoints"] = this.hostPoints;
        data["guestPoints"] = this.guestPoints;
        if (Array.isArray(this.scoresDtos)) {
            data["scoresDtos"] = [];
            for (let item of this.scoresDtos)
                data["scoresDtos"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IOverviewDoubleMatchDto {
    matchIdEncoded?: string;
    hostLeftPlayerName?: string;
    hostRightPlayerName?: string;
    guestLeftPlayerName?: string;
    guestRightPlayerName?: string;
    hostPoints?: number;
    guestPoints?: number;
    scoresDtos?: ScoreDto[];
}

export class UpdateMatchScoreCommand implements IUpdateMatchScoreCommand {
    matchIdEncoded?: string;
    scoreDtos?: ScoreDto[];

    constructor(data?: IUpdateMatchScoreCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.matchIdEncoded = _data["matchIdEncoded"];
            if (Array.isArray(_data["scoreDtos"])) {
                this.scoreDtos = [] as any;
                for (let item of _data["scoreDtos"])
                    this.scoreDtos!.push(ScoreDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): UpdateMatchScoreCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateMatchScoreCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["matchIdEncoded"] = this.matchIdEncoded;
        if (Array.isArray(this.scoreDtos)) {
            data["scoreDtos"] = [];
            for (let item of this.scoreDtos)
                data["scoreDtos"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IUpdateMatchScoreCommand {
    matchIdEncoded?: string;
    scoreDtos?: ScoreDto[];
}

export class CreateTeamMatchCommand implements ICreateTeamMatchCommand {
    hostTeam?: TeamRequest;
    guestTeam?: TeamRequest;

    constructor(data?: ICreateTeamMatchCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.hostTeam = _data["hostTeam"] ? TeamRequest.fromJS(_data["hostTeam"]) : <any>undefined;
            this.guestTeam = _data["guestTeam"] ? TeamRequest.fromJS(_data["guestTeam"]) : <any>undefined;
        }
    }

    static fromJS(data: any): CreateTeamMatchCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateTeamMatchCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["hostTeam"] = this.hostTeam ? this.hostTeam.toJSON() : <any>undefined;
        data["guestTeam"] = this.guestTeam ? this.guestTeam.toJSON() : <any>undefined;
        return data; 
    }
}

export interface ICreateTeamMatchCommand {
    hostTeam?: TeamRequest;
    guestTeam?: TeamRequest;
}

export class TeamRequest implements ITeamRequest {
    name?: string;
    players?: PlayerRequest[];

    constructor(data?: ITeamRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            if (Array.isArray(_data["players"])) {
                this.players = [] as any;
                for (let item of _data["players"])
                    this.players!.push(PlayerRequest.fromJS(item));
            }
        }
    }

    static fromJS(data: any): TeamRequest {
        data = typeof data === 'object' ? data : {};
        let result = new TeamRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        if (Array.isArray(this.players)) {
            data["players"] = [];
            for (let item of this.players)
                data["players"].push(item.toJSON());
        }
        return data; 
    }
}

export interface ITeamRequest {
    name?: string;
    players?: PlayerRequest[];
}

export class PlayerRequest implements IPlayerRequest {
    fullName?: string;
    doublePosition?: DoublePosition;

    constructor(data?: IPlayerRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.fullName = _data["fullName"];
            this.doublePosition = _data["doublePosition"];
        }
    }

    static fromJS(data: any): PlayerRequest {
        data = typeof data === 'object' ? data : {};
        let result = new PlayerRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["fullName"] = this.fullName;
        data["doublePosition"] = this.doublePosition;
        return data; 
    }
}

export interface IPlayerRequest {
    fullName?: string;
    doublePosition?: DoublePosition;
}

export enum DoublePosition {
    None = 0,
    FirstDouble = 1,
    SecondDouble = 2,
}

export class TeamMatchDto implements ITeamMatchDto {
    hostTeamName?: string;
    guestTeamName?: string;
    startedAt?: Date;
    hostVictories?: number;
    guestVictories?: number;
    teamMatchIdEncoded?: string;

    constructor(data?: ITeamMatchDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.hostTeamName = _data["hostTeamName"];
            this.guestTeamName = _data["guestTeamName"];
            this.startedAt = _data["startedAt"] ? new Date(_data["startedAt"].toString()) : <any>undefined;
            this.hostVictories = _data["hostVictories"];
            this.guestVictories = _data["guestVictories"];
            this.teamMatchIdEncoded = _data["teamMatchIdEncoded"];
        }
    }

    static fromJS(data: any): TeamMatchDto {
        data = typeof data === 'object' ? data : {};
        let result = new TeamMatchDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["hostTeamName"] = this.hostTeamName;
        data["guestTeamName"] = this.guestTeamName;
        data["startedAt"] = this.startedAt ? this.startedAt.toISOString() : <any>undefined;
        data["hostVictories"] = this.hostVictories;
        data["guestVictories"] = this.guestVictories;
        data["teamMatchIdEncoded"] = this.teamMatchIdEncoded;
        return data; 
    }
}

export interface ITeamMatchDto {
    hostTeamName?: string;
    guestTeamName?: string;
    startedAt?: Date;
    hostVictories?: number;
    guestVictories?: number;
    teamMatchIdEncoded?: string;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}