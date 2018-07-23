-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 19, 2018 at 12:54 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `crust`
--
CREATE DATABASE IF NOT EXISTS `crust` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `crust`;

-- --------------------------------------------------------

--
-- Table structure for table `dumpsters`
--

DROP TABLE IF EXISTS `dumpsters`;
CREATE TABLE `dumpsters` (
  `dumpster_type` int(11) NOT NULL,
  `dumpster_food` int(11) NOT NULL,
  `dumpster_fix` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `neighborhoods`
--

DROP TABLE IF EXISTS `neighborhoods`;
CREATE TABLE `neighborhoods` (
  `neighborhood_type` int(11) NOT NULL,
  `neighborhood_people` int(11) NOT NULL,
  `neighborhood_shelter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `resources`
--

DROP TABLE IF EXISTS `resources`;
CREATE TABLE `resources` (
  `fix_name` varchar(255) NOT NULL,
  `water_name` varchar(255) NOT NULL,
  `food_name` varchar(255) NOT NULL,
  `shelter_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `hygiene` int(11) NOT NULL,
  `mood` int(11) NOT NULL,
  `rest` int(11) NOT NULL,
  `hunger` int(11) NOT NULL,
  `fix` int(11) NOT NULL,
  `thirst` int(11) NOT NULL,
  `time` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
