--
-- Database creation script
--
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `id_category` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id_category`)
);

--
-- Data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
-- Placeholder data
INSERT INTO `category` VALUES (1,'Fruits et légumes');
-- 
-- ADD CATEGORIES HERE IF NEEDED
--
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
CREATE TABLE `department` (
  `id_department` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_department`)
);

--
-- Data for table `department`
--

-- Due to coding limitations, these departments are need in the final product
LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
-- Placeholder data
INSERT INTO `department` VALUES (1,'Caisses'),(2,'Boucherie'),(3,'Fruits et légumes'),(4,'Boulangerie'),(5,'Produits laitiers'),(6,'Épicerie sèche'),(7,'Entretien ménager'),(8,'Administration');
-- 
-- ADD DEPARTMENT HERE IF NEEDED
--
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `id_employee` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(13) DEFAULT NULL,
  `salary_` decimal(15,2) NOT NULL,
  `designation` varchar(50) NOT NULL,
  `hire_date` date NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `id_department` int NOT NULL,
  PRIMARY KEY (`id_employee`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`),
  KEY `id_department` (`id_department`),
  CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`id_department`) REFERENCES `department` (`id_department`)
);

--
-- Data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
-- Placeholder data
INSERT INTO `employee` VALUES (1,'Admin','admin@admin.ca','1111111111',0,'Admin','2020-02-02','admin','admin123', 8);
-- 
-- ADD EMPLOYEE HERE IF NEEDED
--
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_`
--

DROP TABLE IF EXISTS `order_`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_` (
  `id_order` int NOT NULL AUTO_INCREMENT,
  `date_` datetime NOT NULL,
  `total_price` decimal(15,2) NOT NULL,
  `id_employee` int NOT NULL,
  PRIMARY KEY (`id_order`),
  KEY `id_employee` (`id_employee`),
  CONSTRAINT `order__ibfk_1` FOREIGN KEY (`id_employee`) REFERENCES `employee` (`id_employee`)
);

--
-- Table structure for table `order_details`
--

DROP TABLE IF EXISTS `order_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_details` (
  `id_order_details` int NOT NULL AUTO_INCREMENT,
  `unit_price` decimal(15,2) NOT NULL,
  `quantity` int NOT NULL,
  `id_product` int NOT NULL,
  `id_order` int NOT NULL,
  PRIMARY KEY (`id_order_details`),
  KEY `id_product` (`id_product`),
  KEY `id_order` (`id_order`),
  CONSTRAINT `order_details_ibfk_1` FOREIGN KEY (`id_product`) REFERENCES `product` (`id_product`),
  CONSTRAINT `order_details_ibfk_2` FOREIGN KEY (`id_order`) REFERENCES `order_` (`id_order`)
);

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `id_product` int NOT NULL AUTO_INCREMENT,
  `code` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `price` decimal(15,2) NOT NULL,
  `quantity_in_stock` int NOT NULL,
  `taxable` tinyint(1) DEFAULT NULL,
  `id_subcategory` int NOT NULL,
  PRIMARY KEY (`id_product`),
  UNIQUE KEY `code` (`code`),
  KEY `id_subcategory` (`id_subcategory`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`id_subcategory`) REFERENCES `subcategory` (`id_subcategory`)
);

--
-- Data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
-- Placeholder data
INSERT INTO `product` VALUES (1,'012345000001','Pommes Gala (sac 3 lb)',6.99,50,1,1);
-- 
-- ADD PRODUCT HERE IF NEEDED
--
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subcategory`
--

DROP TABLE IF EXISTS `subcategory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subcategory` (
  `id_subcategory` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `id_category` int NOT NULL,
  PRIMARY KEY (`id_subcategory`),
  KEY `id_category` (`id_category`),
  CONSTRAINT `subcategory_ibfk_1` FOREIGN KEY (`id_category`) REFERENCES `category` (`id_category`)
);

--
-- Data for table `subcategory`
--

LOCK TABLES `subcategory` WRITE;
/*!40000 ALTER TABLE `subcategory` DISABLE KEYS */;
INSERT INTO `subcategory` VALUES (1,'Fruits',1);
-- 
-- ADD SUBCATEGORY HERE IF NEEDED
--
/*!40000 ALTER TABLE `subcategory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;