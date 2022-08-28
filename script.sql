--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2
-- Dumped by pg_dump version 13.2

-- Started on 2022-08-28 00:57:04

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 202 (class 1259 OID 25146)
-- Name: RSS; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RSS" (
    "Id" bigint NOT NULL,
    "LinkId" text,
    "Title" text,
    "Desc" text,
    "Link" text,
    "BaseUrl" text NOT NULL,
    "PublishDate" text,
    "UserID" text NOT NULL,
    "isReaded" boolean NOT NULL,
    "isBookmarked" boolean NOT NULL
);


ALTER TABLE public."RSS" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 25156)
-- Name: RSSComments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RSSComments" (
    "Id" bigint NOT NULL,
    "UserId" text NOT NULL,
    "Link" text NOT NULL,
    "Comment" text NOT NULL
);


ALTER TABLE public."RSSComments" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 25154)
-- Name: RSSComments_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."RSSComments" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."RSSComments_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 201 (class 1259 OID 25144)
-- Name: RSS_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."RSS" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."RSS_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 200 (class 1259 OID 25139)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 25176)
-- Name: userSubscriptions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."userSubscriptions" (
    id bigint NOT NULL,
    "UserId" text NOT NULL,
    "RSSURL" text NOT NULL
);


ALTER TABLE public."userSubscriptions" OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 25174)
-- Name: userSubscriptions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."userSubscriptions" ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."userSubscriptions_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 206 (class 1259 OID 25166)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    "Id" integer NOT NULL,
    "UserName" text NOT NULL,
    "Password" text NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 25164)
-- Name: users_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.users ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."users_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 2879 (class 2606 OID 25153)
-- Name: RSS PK_RSS; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RSS"
    ADD CONSTRAINT "PK_RSS" PRIMARY KEY ("Id");


--
-- TOC entry 2881 (class 2606 OID 25163)
-- Name: RSSComments PK_RSSComments; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RSSComments"
    ADD CONSTRAINT "PK_RSSComments" PRIMARY KEY ("Id");


--
-- TOC entry 2877 (class 2606 OID 25143)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 2885 (class 2606 OID 25183)
-- Name: userSubscriptions PK_userSubscriptions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."userSubscriptions"
    ADD CONSTRAINT "PK_userSubscriptions" PRIMARY KEY (id);


--
-- TOC entry 2883 (class 2606 OID 25173)
-- Name: users PK_users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT "PK_users" PRIMARY KEY ("Id");


-- Completed on 2022-08-28 00:57:05

--
-- PostgreSQL database dump complete
--

