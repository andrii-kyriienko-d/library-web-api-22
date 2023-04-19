--
-- PostgreSQL database dump
--

-- Dumped from database version 14.0
-- Dumped by pg_dump version 14.0

-- Started on 2023-04-19 17:05:44

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

--
-- TOC entry 3349 (class 1262 OID 40960)
-- Name: libraryDb; Type: DATABASE; Schema: -; Owner: libraryuser
--

CREATE DATABASE "libraryDb" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Ukrainian_Ukraine.1251';


ALTER DATABASE "libraryDb" OWNER TO libraryuser;

\connect "libraryDb"

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
-- TOC entry 209 (class 1259 OID 571542)
-- Name: Booklets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Booklets" (
    "Id" integer NOT NULL,
    "FullName" text,
    "Description" text,
    "BookCount" integer,
    "Price" numeric,
    "WikiLink" text,
    "Created" timestamp without time zone,
    "Modified" timestamp without time zone
);


ALTER TABLE public."Booklets" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 571547)
-- Name: Booklets_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Booklets_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Booklets_id_seq" OWNER TO postgres;

--
-- TOC entry 3350 (class 0 OID 0)
-- Dependencies: 210
-- Name: Booklets_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Booklets_id_seq" OWNED BY public."Booklets"."Id";


--
-- TOC entry 211 (class 1259 OID 571548)
-- Name: Books; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Books" (
    "Id" integer NOT NULL,
    "Name" text,
    "Pages" integer,
    "Genre" text,
    "BookletId" integer,
    "PublisherId" integer,
    "Created" timestamp without time zone,
    "Modified" timestamp without time zone
);


ALTER TABLE public."Books" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 571553)
-- Name: Books_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Books_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Books_Id_seq" OWNER TO postgres;

--
-- TOC entry 3351 (class 0 OID 0)
-- Dependencies: 212
-- Name: Books_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Books_Id_seq" OWNED BY public."Books"."Id";


--
-- TOC entry 213 (class 1259 OID 571554)
-- Name: Publishers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Publishers" (
    "Name" text,
    "Id" integer NOT NULL,
    "CompanyUCode" integer,
    "Location" text,
    "Created" timestamp without time zone,
    "Modified" timestamp without time zone
);


ALTER TABLE public."Publishers" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 571559)
-- Name: Publisher_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Publisher_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Publisher_ID_seq" OWNER TO postgres;

--
-- TOC entry 3352 (class 0 OID 0)
-- Dependencies: 214
-- Name: Publisher_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Publisher_ID_seq" OWNED BY public."Publishers"."Id";


--
-- TOC entry 215 (class 1259 OID 571560)
-- Name: ReaderBook; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReaderBook" (
    "Id" integer NOT NULL,
    "ReaderId" integer,
    "BookId" integer,
    "Created" timestamp without time zone,
    "Modified" timestamp without time zone
);


ALTER TABLE public."ReaderBook" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 571563)
-- Name: Readers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Readers" (
    "Id" integer NOT NULL,
    "Name" text,
    "Surname" text,
    "RegistrationDate" date,
    "Age" integer,
    "LibraryCode" text,
    "Created" timestamp without time zone,
    "Modified" timestamp without time zone
);


ALTER TABLE public."Readers" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 571568)
-- Name: Readers_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Readers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Readers_Id_seq" OWNER TO postgres;

--
-- TOC entry 3353 (class 0 OID 0)
-- Dependencies: 217
-- Name: Readers_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Readers_Id_seq" OWNED BY public."Readers"."Id";


--
-- TOC entry 218 (class 1259 OID 571569)
-- Name: reader_to_book_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.reader_to_book_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.reader_to_book_id_seq OWNER TO postgres;

--
-- TOC entry 3354 (class 0 OID 0)
-- Dependencies: 218
-- Name: reader_to_book_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.reader_to_book_id_seq OWNED BY public."ReaderBook"."Id";


--
-- TOC entry 3184 (class 2604 OID 571570)
-- Name: Booklets Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Booklets" ALTER COLUMN "Id" SET DEFAULT nextval('public."Booklets_id_seq"'::regclass);


--
-- TOC entry 3185 (class 2604 OID 571571)
-- Name: Books Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Books" ALTER COLUMN "Id" SET DEFAULT nextval('public."Books_Id_seq"'::regclass);


--
-- TOC entry 3186 (class 2604 OID 571572)
-- Name: Publishers Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Publishers" ALTER COLUMN "Id" SET DEFAULT nextval('public."Publisher_ID_seq"'::regclass);


--
-- TOC entry 3187 (class 2604 OID 571573)
-- Name: ReaderBook Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReaderBook" ALTER COLUMN "Id" SET DEFAULT nextval('public.reader_to_book_id_seq'::regclass);


--
-- TOC entry 3188 (class 2604 OID 571574)
-- Name: Readers Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Readers" ALTER COLUMN "Id" SET DEFAULT nextval('public."Readers_Id_seq"'::regclass);


--
-- TOC entry 3190 (class 2606 OID 571576)
-- Name: Booklets Booklets_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Booklets"
    ADD CONSTRAINT "Booklets_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 3192 (class 2606 OID 571578)
-- Name: Books Books_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "Books_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 3196 (class 2606 OID 571580)
-- Name: Publishers Publisher_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Publishers"
    ADD CONSTRAINT "Publisher_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 3200 (class 2606 OID 571582)
-- Name: Readers Readers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Readers"
    ADD CONSTRAINT "Readers_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 3194 (class 2606 OID 571584)
-- Name: Books Unique_Booklet_Id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "Unique_Booklet_Id" UNIQUE ("BookletId");


--
-- TOC entry 3198 (class 2606 OID 571586)
-- Name: ReaderBook reader_to_book_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReaderBook"
    ADD CONSTRAINT reader_to_book_pkey PRIMARY KEY ("Id");


--
-- TOC entry 3201 (class 2606 OID 571587)
-- Name: Books FK_Booklet_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "FK_Booklet_Id" FOREIGN KEY ("BookletId") REFERENCES public."Booklets"("Id") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 3202 (class 2606 OID 571592)
-- Name: Books FK_Publisher_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "FK_Publisher_Id" FOREIGN KEY ("PublisherId") REFERENCES public."Publishers"("Id") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 3203 (class 2606 OID 571597)
-- Name: ReaderBook FK_book_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReaderBook"
    ADD CONSTRAINT "FK_book_id" FOREIGN KEY ("BookId") REFERENCES public."Books"("Id");


--
-- TOC entry 3204 (class 2606 OID 571602)
-- Name: ReaderBook FK_reader_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReaderBook"
    ADD CONSTRAINT "FK_reader_id" FOREIGN KEY ("ReaderId") REFERENCES public."Readers"("Id");


-- Completed on 2023-04-19 17:05:44

--
-- PostgreSQL database dump complete
--

