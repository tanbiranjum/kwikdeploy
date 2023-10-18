//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming



export class PaginatedListOfAppDefHeadDto implements IPaginatedListOfAppDefHeadDto {
    items?: AppDefHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfAppDefHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(AppDefHeadDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfAppDefHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfAppDefHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfAppDefHeadDto {
    items?: AppDefHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class AppDefHeadDto implements IAppDefHeadDto {
    id?: number;
    name?: string;
    imageName?: string;
    tag?: string;

    constructor(data?: IAppDefHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.imageName = _data["imageName"];
            this.tag = _data["tag"];
        }
    }

    static fromJS(data: any): AppDefHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new AppDefHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["imageName"] = this.imageName;
        data["tag"] = this.tag;
        return data;
    }
}

export interface IAppDefHeadDto {
    id?: number;
    name?: string;
    imageName?: string;
    tag?: string;
}

export class ProblemDetails implements IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    [key: string]: any;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            for (var property in _data) {
                if (_data.hasOwnProperty(property))
                    this[property] = _data[property];
            }
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        for (var property in this) {
            if (this.hasOwnProperty(property))
                data[property] = this[property];
        }
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        return data;
    }
}

export interface IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    [key: string]: any;
}

export class AppDefDto implements IAppDefDto {
    id?: number;
    projectId?: number;
    name?: string;
    imageName?: string;
    tag?: string;

    constructor(data?: IAppDefDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.projectId = _data["projectId"];
            this.name = _data["name"];
            this.imageName = _data["imageName"];
            this.tag = _data["tag"];
        }
    }

    static fromJS(data: any): AppDefDto {
        data = typeof data === 'object' ? data : {};
        let result = new AppDefDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["projectId"] = this.projectId;
        data["name"] = this.name;
        data["imageName"] = this.imageName;
        data["tag"] = this.tag;
        return data;
    }
}

export interface IAppDefDto {
    id?: number;
    projectId?: number;
    name?: string;
    imageName?: string;
    tag?: string;
}

export class ResultOfBoolean implements IResultOfBoolean {
    value?: boolean;

    constructor(data?: IResultOfBoolean) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.value = _data["value"];
        }
    }

    static fromJS(data: any): ResultOfBoolean {
        data = typeof data === 'object' ? data : {};
        let result = new ResultOfBoolean();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["value"] = this.value;
        return data;
    }
}

export interface IResultOfBoolean {
    value?: boolean;
}

export class ResultOfInteger implements IResultOfInteger {
    value?: number;

    constructor(data?: IResultOfInteger) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.value = _data["value"];
        }
    }

    static fromJS(data: any): ResultOfInteger {
        data = typeof data === 'object' ? data : {};
        let result = new ResultOfInteger();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["value"] = this.value;
        return data;
    }
}

export interface IResultOfInteger {
    value?: number;
}

export class AppDefCreateCommandBody implements IAppDefCreateCommandBody {
    name?: string;
    imageName?: string;
    tag?: string;

    constructor(data?: IAppDefCreateCommandBody) {
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
            this.imageName = _data["imageName"];
            this.tag = _data["tag"];
        }
    }

    static fromJS(data: any): AppDefCreateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new AppDefCreateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["imageName"] = this.imageName;
        data["tag"] = this.tag;
        return data;
    }
}

export interface IAppDefCreateCommandBody {
    name?: string;
    imageName?: string;
    tag?: string;
}

export class AppDefUpdateCommandBody implements IAppDefUpdateCommandBody {
    name?: string;
    imageName?: string;
    tag?: string;

    constructor(data?: IAppDefUpdateCommandBody) {
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
            this.imageName = _data["imageName"];
            this.tag = _data["tag"];
        }
    }

    static fromJS(data: any): AppDefUpdateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new AppDefUpdateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["imageName"] = this.imageName;
        data["tag"] = this.tag;
        return data;
    }
}

export interface IAppDefUpdateCommandBody {
    name?: string;
    imageName?: string;
    tag?: string;
}

export class PaginatedListOfEnvHeadDto implements IPaginatedListOfEnvHeadDto {
    items?: EnvHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfEnvHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(EnvHeadDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfEnvHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfEnvHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfEnvHeadDto {
    items?: EnvHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class EnvHeadDto implements IEnvHeadDto {
    id?: number;
    targetName?: string;
    name?: string;

    constructor(data?: IEnvHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.targetName = _data["targetName"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): EnvHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new EnvHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["targetName"] = this.targetName;
        data["name"] = this.name;
        return data;
    }
}

export interface IEnvHeadDto {
    id?: number;
    targetName?: string;
    name?: string;
}

export class EnvDto implements IEnvDto {
    id?: number;
    projectId?: number;
    targetId?: number;
    name?: string;

    constructor(data?: IEnvDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.projectId = _data["projectId"];
            this.targetId = _data["targetId"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): EnvDto {
        data = typeof data === 'object' ? data : {};
        let result = new EnvDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["projectId"] = this.projectId;
        data["targetId"] = this.targetId;
        data["name"] = this.name;
        return data;
    }
}

export interface IEnvDto {
    id?: number;
    projectId?: number;
    targetId?: number;
    name?: string;
}

export class EnvCreateCommandBody implements IEnvCreateCommandBody {
    targetId?: number;
    name?: string;

    constructor(data?: IEnvCreateCommandBody) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.targetId = _data["targetId"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): EnvCreateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new EnvCreateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["targetId"] = this.targetId;
        data["name"] = this.name;
        return data;
    }
}

export interface IEnvCreateCommandBody {
    targetId?: number;
    name?: string;
}

export class EnvUpdateCommandBody implements IEnvUpdateCommandBody {
    targetId?: number;
    name?: string;

    constructor(data?: IEnvUpdateCommandBody) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.targetId = _data["targetId"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): EnvUpdateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new EnvUpdateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["targetId"] = this.targetId;
        data["name"] = this.name;
        return data;
    }
}

export interface IEnvUpdateCommandBody {
    targetId?: number;
    name?: string;
}

export class PaginatedListOfProjectHeadDto implements IPaginatedListOfProjectHeadDto {
    items?: ProjectHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfProjectHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(ProjectHeadDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfProjectHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfProjectHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfProjectHeadDto {
    items?: ProjectHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class ProjectHeadDto implements IProjectHeadDto {
    id?: number;
    name?: string;

    constructor(data?: IProjectHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): ProjectHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data;
    }
}

export interface IProjectHeadDto {
    id?: number;
    name?: string;
}

export class ProjectDto implements IProjectDto {
    id?: number;
    name?: string;

    constructor(data?: IProjectDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): ProjectDto {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data;
    }
}

export interface IProjectDto {
    id?: number;
    name?: string;
}

export class ProjectCreateCommandBody implements IProjectCreateCommandBody {
    name?: string;

    constructor(data?: IProjectCreateCommandBody) {
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
        }
    }

    static fromJS(data: any): ProjectCreateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectCreateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface IProjectCreateCommandBody {
    name?: string;
}

export class ProjectUpdateCommandBody implements IProjectUpdateCommandBody {
    name?: string;

    constructor(data?: IProjectUpdateCommandBody) {
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
        }
    }

    static fromJS(data: any): ProjectUpdateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectUpdateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface IProjectUpdateCommandBody {
    name?: string;
}

export class PaginatedListOfTargetHeadDto implements IPaginatedListOfTargetHeadDto {
    items?: TargetHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfTargetHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(TargetHeadDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfTargetHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfTargetHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfTargetHeadDto {
    items?: TargetHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class TargetHeadDto implements ITargetHeadDto {
    id?: number;
    name?: string;
    connected?: boolean;

    constructor(data?: ITargetHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.connected = _data["connected"];
        }
    }

    static fromJS(data: any): TargetHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new TargetHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["connected"] = this.connected;
        return data;
    }
}

export interface ITargetHeadDto {
    id?: number;
    name?: string;
    connected?: boolean;
}

export class TargetDto implements ITargetDto {
    id?: number;
    projectId?: number;
    name?: string;

    constructor(data?: ITargetDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.projectId = _data["projectId"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): TargetDto {
        data = typeof data === 'object' ? data : {};
        let result = new TargetDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["projectId"] = this.projectId;
        data["name"] = this.name;
        return data;
    }
}

export interface ITargetDto {
    id?: number;
    projectId?: number;
    name?: string;
}

export class TargetCreateCommandBody implements ITargetCreateCommandBody {
    name?: string;

    constructor(data?: ITargetCreateCommandBody) {
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
        }
    }

    static fromJS(data: any): TargetCreateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new TargetCreateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface ITargetCreateCommandBody {
    name?: string;
}

export class TargetUpdateCommandBody implements ITargetUpdateCommandBody {
    name?: string;

    constructor(data?: ITargetUpdateCommandBody) {
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
        }
    }

    static fromJS(data: any): TargetUpdateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new TargetUpdateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface ITargetUpdateCommandBody {
    name?: string;
}

export class ResultOfString implements IResultOfString {
    value?: string | undefined;

    constructor(data?: IResultOfString) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.value = _data["value"];
        }
    }

    static fromJS(data: any): ResultOfString {
        data = typeof data === 'object' ? data : {};
        let result = new ResultOfString();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["value"] = this.value;
        return data;
    }
}

export interface IResultOfString {
    value?: string | undefined;
}

export class SuccessfulLoginResponse implements ISuccessfulLoginResponse {
    id?: string;
    username?: string;
    email?: string;
    token?: string;

    constructor(data?: ISuccessfulLoginResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.username = _data["username"];
            this.email = _data["email"];
            this.token = _data["token"];
        }
    }

    static fromJS(data: any): SuccessfulLoginResponse {
        data = typeof data === 'object' ? data : {};
        let result = new SuccessfulLoginResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["username"] = this.username;
        data["email"] = this.email;
        data["token"] = this.token;
        return data;
    }
}

export interface ISuccessfulLoginResponse {
    id?: string;
    username?: string;
    email?: string;
    token?: string;
}

export class UserLogin implements IUserLogin {
    userName?: string;
    password?: string;

    constructor(data?: IUserLogin) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userName = _data["userName"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): UserLogin {
        data = typeof data === 'object' ? data : {};
        let result = new UserLogin();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["password"] = this.password;
        return data;
    }
}

export interface IUserLogin {
    userName?: string;
    password?: string;
}

export class PaginatedListOfUserHeadDto implements IPaginatedListOfUserHeadDto {
    items?: UserHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfUserHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(UserHeadDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfUserHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfUserHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfUserHeadDto {
    items?: UserHeadDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class UserHeadDto implements IUserHeadDto {
    id?: string;
    userName?: string | undefined;

    constructor(data?: IUserHeadDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.userName = _data["userName"];
        }
    }

    static fromJS(data: any): UserHeadDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserHeadDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["userName"] = this.userName;
        return data;
    }
}

export interface IUserHeadDto {
    id?: string;
    userName?: string | undefined;
}

export class UserDto implements IUserDto {
    id?: string;
    userName?: string | undefined;
    email?: string | undefined;
    emailConfirmed?: boolean;
    lockoutEnd?: Date | undefined;
    lockoutEnabled?: boolean;
    accessFailedCount?: number;

    constructor(data?: IUserDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.userName = _data["userName"];
            this.email = _data["email"];
            this.emailConfirmed = _data["emailConfirmed"];
            this.lockoutEnd = _data["lockoutEnd"] ? new Date(_data["lockoutEnd"].toString()) : <any>undefined;
            this.lockoutEnabled = _data["lockoutEnabled"];
            this.accessFailedCount = _data["accessFailedCount"];
        }
    }

    static fromJS(data: any): UserDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["userName"] = this.userName;
        data["email"] = this.email;
        data["emailConfirmed"] = this.emailConfirmed;
        data["lockoutEnd"] = this.lockoutEnd ? this.lockoutEnd.toISOString() : <any>undefined;
        data["lockoutEnabled"] = this.lockoutEnabled;
        data["accessFailedCount"] = this.accessFailedCount;
        return data;
    }
}

export interface IUserDto {
    id?: string;
    userName?: string | undefined;
    email?: string | undefined;
    emailConfirmed?: boolean;
    lockoutEnd?: Date | undefined;
    lockoutEnabled?: boolean;
    accessFailedCount?: number;
}

export class UserCreateCommandBody implements IUserCreateCommandBody {
    userName?: string;
    email?: string | undefined;
    password?: string;
    confirmPassword?: string;

    constructor(data?: IUserCreateCommandBody) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userName = _data["userName"];
            this.email = _data["email"];
            this.password = _data["password"];
            this.confirmPassword = _data["confirmPassword"];
        }
    }

    static fromJS(data: any): UserCreateCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new UserCreateCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["email"] = this.email;
        data["password"] = this.password;
        data["confirmPassword"] = this.confirmPassword;
        return data;
    }
}

export interface IUserCreateCommandBody {
    userName?: string;
    email?: string | undefined;
    password?: string;
    confirmPassword?: string;
}

export class UserSetEmailCommandBody implements IUserSetEmailCommandBody {
    email?: string;

    constructor(data?: IUserSetEmailCommandBody) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
        }
    }

    static fromJS(data: any): UserSetEmailCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new UserSetEmailCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        return data;
    }
}

export interface IUserSetEmailCommandBody {
    email?: string;
}

export class UserSetUserNameCommandBody implements IUserSetUserNameCommandBody {
    userName?: string;

    constructor(data?: IUserSetUserNameCommandBody) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userName = _data["userName"];
        }
    }

    static fromJS(data: any): UserSetUserNameCommandBody {
        data = typeof data === 'object' ? data : {};
        let result = new UserSetUserNameCommandBody();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        return data;
    }
}

export interface IUserSetUserNameCommandBody {
    userName?: string;
}